using System;
using System.Windows;
using PREMIUM_KINO.Classes;


namespace PREMIUM_KINO
{
    public partial class SetRating : Window
    {
        UserOrder order;
        private UnitOfWork context;
        private static Window thisWindow;

        public SetRating()
        {
            thisWindow = this;
            InitializeComponent();
            context = new UnitOfWork();
            order = UserOrder.getInstance();
        }

        public SetRating(UserOrder order)
        {
            thisWindow = this;
            InitializeComponent();
            this.order = UserOrder.getInstance();
            this.order = order;
            context = new UnitOfWork();
        }



        private void setRating_Click(object sender, RoutedEventArgs e)
        {
            int rate;
            try
            {
                if (!Int32.TryParse(inputRating.Text, out rate))
                    MessageBox.Show("Введите корректное численное значение.", "Ошибка!", MessageBoxButton.OK);
                else if (rate <= 0)
                    MessageBox.Show("Введите положительное значение.", "Ошибка!", MessageBoxButton.OK);
                else if (rate > 10)
                    MessageBox.Show("Введите значение от 1 до 10.", "Ошибка!", MessageBoxButton.OK);
                else if (string.IsNullOrEmpty(inputRating.Text))
                    MessageBox.Show("Пожалуйста, введите значение.", "Ошибка!", MessageBoxButton.OK);
                else
                {
                    var res = context.MovieRepo.UpdateRating(order, rate);
                    if (res)
                    {
                        MessageBox.Show($"Вы успешно оценили фильм на {rate}!", "Успешно!", MessageBoxButton.OK);
                        this.Close();
                    }
                    else throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Возникла ошибка. Повторите попытку позже", "Ошибка!", MessageBoxButton.OK);
            }
        }

        public static void closeWindow() => thisWindow.Close();

    }
}
