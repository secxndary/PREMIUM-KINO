using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using PREMIUM_KINO.Classes;
using PREMIUM_KINO.EFCore;
using PREMIUM_KINO.EFCore.Entities;

namespace PREMIUM_KINO
{
    public partial class MainFrameUser : Page
    {
        private bool styleCheck = false;
        private List<Movie> listOfFilms;
        private UnitOfWork context;

        public MainFrameUser()
        {
            InitializeComponent();
            var sri = Application.GetResourceStream(new Uri("./Styles/arrow.cur", UriKind.Relative));
            var customCursor = new Cursor(sri.Stream);
            Cursor = customCursor;
            App.LanguageChanged += languageChanged;
            CultureInfo currLang = App.Language;

            context = new UnitOfWork();
            listOfFilms = context.MovieRepo.GetAllMovies();
            mainFilmsListView.ItemsSource = listOfFilms;
        }


        private void languageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;
        }

        private void buttonRu_Click(object sender, RoutedEventArgs e)
        {
            CultureInfo lang = new CultureInfo("ru-RU");
            App.Language = lang;
        }

        private void buttonEng_Click(object sender, RoutedEventArgs e)
        {
            CultureInfo lang = new CultureInfo("en-US");
            App.Language = lang;
        }


        private void darkTheme_Click(object sender, RoutedEventArgs e)
        {
            if (styleCheck)
            {
                var uri = new Uri("./Styles/BlackTheme.xaml", UriKind.Relative);
                var resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
                Application.Current.Resources.Clear();
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
                styleCheck = false;
            }
            else
            {
                var uri = new Uri("./Styles/WhiteTheme.xaml", UriKind.Relative);
                var resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
                Application.Current.Resources.Clear();
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
                styleCheck = true;
            }
        }


        private void buyTicket_Click(object sender, RoutedEventArgs e)
        { 

            NavigationService.Navigate((new Uri("./ReserveTicket.xaml", UriKind.Relative)));
            var selectedFilm = (sender as Button)?.DataContext as Movie;
            var tickets = context.ScheduleRepo.GetMovieTickets(selectedFilm);
            Application.Current.Properties.Add("selectedFilm", selectedFilm);
        }



        private void filterGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedBoxItem = filterGenre.SelectedValue as ComboBoxItem;
            string selectedGenre = selectedBoxItem.Content.ToString();
            var regex = new Regex(@"(\w)*" + selectedGenre + @"(\w*)", RegexOptions.IgnoreCase);
            var newList = new List<EFCore.Entities.Movie>();

            foreach (var movie in listOfFilms)
            {
                var matches = regex.Matches(movie.Genre);
                if (matches.Count > 0)
                    newList.Add(movie);
            }
            mainFilmsListView.ItemsSource = newList;

            if (selectedGenre == "Все жанры" || selectedGenre == "All genres")
                mainFilmsListView.ItemsSource = listOfFilms;
        }



        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox txtBox = e.Source as TextBox;
                var searchText = txtBox.Text;
                var listSearch = new List<EFCore.Entities.Movie>();
                var regex = new Regex(@"(\w)*" + searchText + @"(\w*)", RegexOptions.IgnoreCase);

                if (txtBox != null)
                {
                    foreach (var movie in listOfFilms)
                    {
                        var matchesTitle = regex.Matches(movie.Title);
                        var matchesDir = regex.Matches(movie.Director);
                        if (matchesTitle.Count > 0 || matchesDir.Count > 0)
                            listSearch.Add(movie);
                    }
                    mainFilmsListView.ItemsSource = listSearch;
                }
            }
        }



        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = searchTextBox.Text;
            var listSearch = new List<EFCore.Entities.Movie>();
            var regex = new Regex(@"(\w)*" + searchText + @"(\w*)", RegexOptions.IgnoreCase);

            foreach (var movie in listOfFilms)
            {
                var matchesTitle = regex.Matches(movie.Title);
                var matchesDir = regex.Matches(movie.Director);
                if (matchesTitle.Count > 0 || matchesDir.Count > 0)
                    listSearch.Add(movie);
            }
            mainFilmsListView.ItemsSource = listSearch;
        }
    }
}
