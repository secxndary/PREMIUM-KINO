using System;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Input;
using PREMIUM_KINO.Classes;
using PREMIUM_KINO.EFCore.Entities;

namespace PREMIUM_KINO
{
    public partial class ReserveTicketForm : Window
    {
        private UnitOfWork context;
        public Users userSignedIn;
        private Ticket selectedSchedule;
        private static Window thisWindow;


        public ReserveTicketForm()
        {
            thisWindow = this;
            InitializeComponent();
            context = new UnitOfWork();
            selectedSchedule = Ticket.getInstance();
            selectedSchedule = (Ticket)Application.Current.Properties["selectedSchedule"];
            Application.Current.Properties.Remove("selectedSchedule");
            userSignedIn = Application.Current.Properties["userSignedIn"] as Users;
            DataContext = selectedSchedule;
            var sri = Application.GetResourceStream(new Uri("./Styles/arrow.cur", UriKind.Relative));
            var customCursor = new Cursor(sri.Stream);
            Cursor = customCursor;
        }



        private void orderSomeTickets_Click(object sender, RoutedEventArgs e)
        {
            var text = selectCountOfSeats.Text;
            int countSeats = 0;
            try
            {
                countSeats = Int32.Parse(selectCountOfSeats.Text);
                if (countSeats > selectedSchedule.Aviable_Seats)
                    MessageBox.Show("Вы не можете заказать больше доступного кол-ва мест.", "Ошибка!", MessageBoxButton.OK);
                else if (countSeats > 10)
                    MessageBox.Show("Вы не можете заказать больше 10 мест.", "Ошибка!", MessageBoxButton.OK);
                else if (countSeats <= 0)
                    MessageBox.Show("Вы не можете заказать меньше 0 мест.", "Ошибка!", MessageBoxButton.OK);
                else
                {
                    try
                    {
                        context.OrdersRepo.AddOrder(Guid.NewGuid(), selectedSchedule, userSignedIn, countSeats);
                        SendMail(countSeats);
                        MessageBox.Show("Вы успешно заказали билет!", "Успешно!", MessageBoxButton.OK);
                        this.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Произошла ошибка при заказе билета. Повторите попытку позже.", "Ошибка!", MessageBoxButton.OK);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Пожалуйста, введите корректное значение.", "Ошибка!", MessageBoxButton.OK);
            }
        }


        private void SendMail(int count)
        {
            var countSeats = count;
            var mailFrom = new MailAddress("premiumkinobot@gmail.com", "ПРЕМИУМ КИНО");
            var mailTo = new MailAddress(userSignedIn.Email, userSignedIn.Name);
            var message = new MailMessage(mailFrom, mailTo);
            message.Body = $"Здравствуйте, {userSignedIn.Name}! Вы успешно забронировали {countSeats} мест(а) на следующий сеанс:\n" +
                $"{selectedSchedule.Title}, {selectedSchedule.DateTime.ToLongDateString()}, {selectedSchedule.DateTime.ToShortTimeString()}." +
                $"\nПриятного просмотра! Пожалуйста, оставьте отзыв о фильме после сеанса.";
            message.Subject = "Бронирование билета: успешно!";

            var client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(mailFrom.Address, "GxJKl7355");
            client.Send(message);
        }


        public static void closeWindow() => thisWindow.Close();
    }
}
