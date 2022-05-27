using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Microsoft.EntityFrameworkCore;
using PREMIUM_KINO.Classes;
using PREMIUM_KINO.EFCore;
using PREMIUM_KINO.EFCore.Entities;

namespace PREMIUM_KINO
{
    public partial class MainWindowUser : Window
    {
        public Users userSignedIn;
        private static Window thisWindow;


        public MainWindowUser()
        {
            thisWindow = this;
            InitializeComponent();
            var sri = Application.GetResourceStream(new Uri("./Styles/arrow.cur", UriKind.Relative));
            var customCursor = new Cursor(sri.Stream);
            Cursor = customCursor;
        }


        public MainWindowUser(Users user)
        {
            thisWindow = this;
            InitializeComponent();
            var sri = Application.GetResourceStream(new Uri("./Styles/arrow.cur", UriKind.Relative));
            var customCursor = new Cursor(sri.Stream);
            Cursor = customCursor;

            userSignedIn = user;
            if (userSignedIn.Role == 1)
                adminPanel.Visibility = Visibility.Hidden;
        }

        public MainWindowUser(Users user, Window window)
        {
            thisWindow = this;
            InitializeComponent();
            window.Close();
            var sri = Application.GetResourceStream(new Uri("./Styles/arrow.cur", UriKind.Relative));
            var customCursor = new Cursor(sri.Stream);
            Cursor = customCursor;

            userSignedIn = user;
            if (userSignedIn.Role == 1)
                adminPanel.Visibility = Visibility.Hidden;
        }



        public static void closeWindow() => thisWindow.Close();

        private void afisha_Click(object sender, RoutedEventArgs e) => frameMain.NavigationService.Navigate(new Uri("./MainFrame.xaml", UriKind.Relative));

        private void adminPanel_Click(object sender, RoutedEventArgs e) => frameMain.NavigationService.Navigate(new Uri("./AdminPanel.xaml", UriKind.Relative));

        private void cabinet_Click(object sender, RoutedEventArgs e) => frameMain.NavigationService.Navigate(new Uri("./PersonalAccount.xaml", UriKind.Relative));

        private void aboutUs_Click(object sender, RoutedEventArgs e) => frameMain.NavigationService.Navigate(new Uri("./AboutUs.xaml", UriKind.Relative));

        private void Window_Unloaded(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
    }
}
