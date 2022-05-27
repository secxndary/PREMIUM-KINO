using System.Windows;
using System.Windows.Controls;
using PREMIUM_KINO.Classes;
using PREMIUM_KINO.EFCore.Entities;


namespace PREMIUM_KINO
{
    public partial class PersonalAccount : Page
    {
        private Users userSignedIn;
        private UnitOfWork context;


        public PersonalAccount()
        {
            InitializeComponent();
            context = new UnitOfWork();
            userSignedIn = Application.Current.Properties["userSignedIn"] as Users;
            DataContext = userSignedIn;
            if (userSignedIn.Role != 0)
            {
                adminListView.Visibility = Visibility.Hidden;
                listViewOrders.ItemsSource = context.OrdersRepo.GetUserOrders(userSignedIn);

            }
            else
            {
                listViewOrders.Visibility = Visibility.Hidden;
                adminListView.ItemsSource = context.OrdersRepo.GetAllOrders(userSignedIn);
            }
        }


        
        private void ratingButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = (sender as Button)?.DataContext as UserOrder;
            var movieName = selectedOrder.Title;
            SetRating rate = new SetRating(selectedOrder);
            if (selectedOrder.Order_Status != "Просмотрено")
                MessageBox.Show("Вы можете поставить оценку только для просмотренного фильма.", "Ошибка!", MessageBoxButton.OK);
            else
                rate.Show();
        }
    }
}
