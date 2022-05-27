using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PREMIUM_KINO.Classes;
using PREMIUM_KINO.EFCore;
using PREMIUM_KINO.EFCore.Entities;

namespace PREMIUM_KINO
{
    public partial class Login : Page
    {
        private UnitOfWork context;

        public Login() 
        {
            InitializeComponent();
            context = new UnitOfWork();
        }



        private void signInButton_Click(object sender, RoutedEventArgs e)
        {
            var login = loginTextBox.Text;
            var password = SecureStringToString(passwordText.SecurePassword);

            if (string.IsNullOrEmpty(login))
                MessageBox.Show("Введите логин!", "Ошибка!", MessageBoxButton.OK);
            else if (string.IsNullOrEmpty(password))
                MessageBox.Show("Введите пароль!", "Ошибка!", MessageBoxButton.OK);
            else
            {
                var user = context.UsersRepo.GetUserByLogin(login);
                if (user == null)
                    MessageBox.Show("Пользователь с таким логином отсутствует.", "Ошибка!", MessageBoxButton.OK);
                else if (user.Password != password)
                    MessageBox.Show("Неверный пароль.", "Ошибка!", MessageBoxButton.OK);
                else
                {
                    Application.Current.Properties.Remove("userSignedIn");
                    Application.Current.Properties.Add("userSignedIn", user);
                    try
                    {
                        var thisWindow = Window.GetWindow(this);
                        var mainWindowUser = new MainWindowUser(user, thisWindow);
                        mainWindowUser.Show();
                    }
                    catch
                    {
                        MessageBox.Show("Возникла ошибка. Пожалуйста, повторирте попытку поззже", "Ошибка!", MessageBoxButton.OK);
                    }
                }
            }
        }


        private void register_Click(object sender, RoutedEventArgs e) => NavigationService.Navigate((new Uri("./Registration.xaml", UriKind.Relative)));


        private static string SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }

        }
    }
}
