using System;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using PREMIUM_KINO.EFCore.Entities;


namespace PREMIUM_KINO
{
    public partial class RedactingFilm : Window
    {
        public Movie _film;
        private OpenFileDialog openFileDialog;
        private static Window thisWindow;


        public RedactingFilm()
        {
            thisWindow = this;
            InitializeComponent();
            var sri = Application.GetResourceStream(new Uri("./Styles/arrow.cur", UriKind.Relative));
            var customCursor = new Cursor(sri.Stream);
            Cursor = customCursor;
        }


        // Добавить фильм
        private void addFilmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _film = new Movie(filmName.Text, filmDirector.Text, genre.Text,
                int.Parse(duration.Text), float.Parse(rating.Text), openFileDialog.FileName);

                MessageBox.Show($"Название: {filmName.Text}\n" +
                    $"Режиссёр: {filmDirector.Text}\nЖанр: {genre.Text}" +
                    $"\nДлительность: {duration.Text}\nРейтинг: " +
                    $"{rating.Text}\nПуть к фото: {openFileDialog.FileName}",
                    "Изменён фильм", MessageBoxButton.OK);

                closeWindow();
            }
            catch (Exception)
            {
                MessageBox.Show($"Заполните все поля.",
                    "Ошибка!", MessageBoxButton.OK);;
            }
            
        }


        // Добавить фото в фильм
        private void addPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image|*.jpg;*.jpeg;*.png;";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    preview.Source = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
                }
                catch
                {
                    MessageBox.Show("Выберите файл подходящего формата.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        // Очистить форму
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            filmName.Text = "";
            filmDirector.Text = "";
            genre.Text = "";
            duration.Text = "";
            rating.Text = "";
            preview.Source = null;
        }


        public static void closeWindow() => thisWindow.Close();

    }
}
