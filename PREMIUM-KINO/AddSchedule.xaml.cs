using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PREMIUM_KINO.Classes;
using PREMIUM_KINO.EFCore;
using PREMIUM_KINO.EFCore.Entities;

namespace PREMIUM_KINO
{
    public partial class AddSchedule : Window
    {
        private Movie movie;
        private UnitOfWork context;


        public AddSchedule()
        {
            InitializeComponent();
            var sri = Application.GetResourceStream(new Uri("./Styles/arrow.cur", UriKind.Relative));
            var customCursor = new Cursor(sri.Stream);
            Cursor = customCursor;
            context = new UnitOfWork();
        }

        public AddSchedule(Movie movie)
        {
            InitializeComponent();
            var sri = Application.GetResourceStream(new Uri("./Styles/arrow.cur", UriKind.Relative));
            var customCursor = new Cursor(sri.Stream);
            Cursor = customCursor;
            this.movie = movie;
            DataContext = movie;
            context = new UnitOfWork();
        }


        private void addSchedule_Click(object sender, RoutedEventArgs e)
        {
            int seatsInt;
            var date = dateInput.Text;
            var time = timeInput.Text;
            Int32.TryParse(seatsInput.Text, out seatsInt);
            if (string.IsNullOrEmpty(date))
                MessageBox.Show("Введите дату!", "Ошибка", MessageBoxButton.OK);
            else if (string.IsNullOrEmpty(time))
                MessageBox.Show("Введите время!", "Ошибка", MessageBoxButton.OK);
            else if(string.IsNullOrEmpty(seatsInput.Text))
                MessageBox.Show("Введите кол-во мест!", "Ошибка", MessageBoxButton.OK);
            else
            {
                try
                {
                    var dateTest = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                catch
                {
                    MessageBox.Show("Неккоректно введена дата.", "Ошибка!", MessageBoxButton.OK);
                }
                try
                {
                    var timeTest = DateTime.ParseExact(time, "HH:mm", CultureInfo.InvariantCulture);
                }
                catch
                {
                    MessageBox.Show("Неккоректно введено время.", "Ошибка!", MessageBoxButton.OK);
                }

                if (seatsInt > 50)
                    MessageBox.Show("Слишком много мест.", "Ошибка", MessageBoxButton.OK);
                else if (seatsInt < 10)
                    MessageBox.Show("Слишком мало мест.", "Ошибка", MessageBoxButton.OK);
                else if (!Int32.TryParse(seatsInput.Text, out seatsInt))
                    MessageBox.Show("Введите корректное значение доступных мест.", "Ошибка", MessageBoxButton.OK);

                else
                {
                    try
                    {
                        var s = string.Concat(date, " ", time);
                        DateTime dt = DateTime.ParseExact(s, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                        var check = context.ScheduleRepo.AddSchedule(Guid.NewGuid(), movie.Id, seatsInt, dt);
                        MessageBox.Show($"Вы успешно добавили расписание на фильм {movie.Title}!");
                        this.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Возникла ошибка. Пожалуйста, повторите попытку позже.", "Ошибка", MessageBoxButton.OK);
                    }
                }
            }
        }



        private void escButton_Click(object sender, RoutedEventArgs e) => this.Close(); // закрытие окна
    }
}
