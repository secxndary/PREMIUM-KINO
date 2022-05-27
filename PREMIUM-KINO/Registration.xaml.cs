using System;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using PREMIUM_KINO.Classes;


namespace PREMIUM_KINO
{
    public partial class Registration: Page
    {
        private UnitOfWork context;

        public Registration()
        {
            InitializeComponent();
            context = new UnitOfWork();
        }


        private void signUpButton_Click(object sender, RoutedEventArgs e)
        {
            var regex = new Regex(@"^((\+375)((29)||(33)||(44))\d{7})$");
            if (!regex.IsMatch(phoneText.Text))
                MessageBox.Show("Введите корректный номер телефона в формате +375XXXXXXXXX.", "Ошибка!", MessageBoxButton.OK);
            else if (string.IsNullOrEmpty(nameText.Text) || string.IsNullOrEmpty(surnameText.Text) || string.IsNullOrEmpty(loginText.Text) ||
                string.IsNullOrEmpty(SecureStringToString(passwordText.SecurePassword)) || string.IsNullOrEmpty(emailText.Text) || string.IsNullOrEmpty(phoneText.Text))
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка!", MessageBoxButton.OK);
            else if (!isValidMail(emailText.Text))
                MessageBox.Show("Введите корректный e-mail.", "Ошибка!", MessageBoxButton.OK);
            else
            {
                try
                {
                    var result = context.UsersRepo.AddNewUser(Guid.NewGuid(), nameText.Text, surnameText.Text, loginText.Text,
                        SecureStringToString(passwordText.SecurePassword), emailText.Text, phoneText.Text);
                    if (result)
                    {
                        MessageBox.Show("Вы успешно зарегистрировались!", "Успешно!", MessageBoxButton.OK);
                        NavigationService.Navigate((new Uri("./Login.xaml", UriKind.Relative)));
                    }
                    else throw new Exception();
                }
                catch
                {
                    MessageBox.Show("Такой пользователь уже существует.", "Ошибка!", MessageBoxButton.OK);
                }
            }
            
        }

        private void loginButton_Click(object sender, RoutedEventArgs e) => NavigationService.Navigate((new Uri("./Login.xaml", UriKind.Relative)));

        private bool isValidMail(string mail)
        {
            var regex = new Regex(@"^(\w+\@\w+\.\w+)$");
            try
            {
                MailAddress m = new MailAddress(mail);
                if (!regex.IsMatch(mail))
                    return false;
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }


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
