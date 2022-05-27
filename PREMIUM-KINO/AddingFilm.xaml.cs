using System;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using PREMIUM_KINO.EFCore;
using PREMIUM_KINO.Classes;

namespace PREMIUM_KINO
{
    public partial class AddingFilm : Window
    {
        private OpenFileDialog openFileDialog;
        private UnitOfWork context;
        private static Window thisWindow;

        public AddingFilm()
        {
            thisWindow = this;
            InitializeComponent();
            var sri = Application.GetResourceStream(new Uri("./Styles/arrow.cur", UriKind.Relative));
            var customCursor = new Cursor(sri.Stream);
            Cursor = customCursor;
            context = new UnitOfWork();
        }




        // Добавить фильм
        private void addFilmButton_Click(object sender, RoutedEventArgs e)
        {
            int durationInt;
            float ratingFloat;

            if (string.IsNullOrEmpty(filmName.Text) || string.IsNullOrEmpty(filmDirector.Text) || string.IsNullOrEmpty(genre.Text)
                || string.IsNullOrEmpty(duration.Text) || string.IsNullOrEmpty(rating.Text))
                MessageBox.Show("Заполните все поля.", "Ошибка!", MessageBoxButton.OK);
            else if (!int.TryParse(duration.Text, out durationInt) || durationInt <= 0 || durationInt > 400)
                MessageBox.Show("Введите корректное значение длительности.", "Ошибка!", MessageBoxButton.OK);
            else if (!float.TryParse(rating.Text, out ratingFloat))
                MessageBox.Show("Введите корректное значение рейтинга в формате XX,XX.", "Ошибка!", MessageBoxButton.OK);
            else if (ratingFloat < 1 || ratingFloat > 10)
                MessageBox.Show("Введите значение рейтинга от 1 до 10.", "Ошибка!", MessageBoxButton.OK);

            else
            {
                try
                {
                    var movie = new EFCore.Entities.Movie(filmName.Text, filmDirector.Text,
                        genre.Text, durationInt, ratingFloat, openFileDialog.FileName);
                    context.MovieRepo.AddMovie(movie);

                    MessageBox.Show($"Название: {filmName.Text}\n" +
                        $"Режиссёр: {filmDirector.Text}\nЖанр: {genre.Text}" +
                        $"\nДлительность: {duration.Text}\nРейтинг: " +
                        $"{rating.Text}\nПуть к фото: {openFileDialog.FileName}",
                        "Добавлен фильм", MessageBoxButton.OK);
                }
                catch (Exception)
                {
                    MessageBox.Show($"Возникла ошибка. Пожалуйста, повторите попытку позже",
                        "Ошибка!", MessageBoxButton.OK); ;
                }
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
