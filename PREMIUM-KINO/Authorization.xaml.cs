using System;
using System.Windows;
using System.Windows.Input;

namespace PREMIUM_KINO
{
    public partial class Authorization : Window
    {
        private static Window thisWindow;

        public Authorization()
        {
            thisWindow = this;
            InitializeComponent();
            var sri = Application.GetResourceStream(new Uri("./Styles/arrow.cur", UriKind.Relative));
            var customCursor = new Cursor(sri.Stream);
            Cursor = customCursor;
        }

        public static void closeWindow() => thisWindow.Close();
    }
}
