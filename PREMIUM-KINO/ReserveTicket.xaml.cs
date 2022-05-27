using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PREMIUM_KINO.Classes;
using PREMIUM_KINO.EFCore.Entities;

namespace PREMIUM_KINO
{
    public partial class ReserveTicket : Page
    {
        private UnitOfWork context;
        public Users userSignedIn;

        public ReserveTicket()
        {
            InitializeComponent();
            var sri = Application.GetResourceStream(new Uri("./Styles/arrow.cur", UriKind.Relative));
            var customCursor = new Cursor(sri.Stream);
            Cursor = customCursor;
            context = new UnitOfWork();

            var selectedFilm = (Movie)Application.Current.Properties["selectedFilm"];
            Application.Current.Properties.Remove("selectedFilm");
            var tickets = context.ScheduleRepo.GetMovieTickets(selectedFilm);
            OrderTicketListView.ItemsSource = tickets;
            DataContext = selectedFilm;

        }


        private void selectTimeOrder_Click(object sender, RoutedEventArgs e)
        {
            var selectedSchedule = (sender as Button)?.DataContext as Ticket;
            Application.Current.Properties.Add("selectedSchedule", selectedSchedule);

            var reserveTicketForm = new ReserveTicketForm();
            reserveTicketForm.DataContext = selectedSchedule;
            reserveTicketForm.Show();
        }
    }
}
