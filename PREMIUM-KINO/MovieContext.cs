using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PREMIUM_KINO.EFCore;
using PREMIUM_KINO.EFCore.Entities;

namespace PREMIUM_KINO
{
    public class MovieContext : DBContext
    {
        public static List<Movie> getAllMovies()
        {
            var listOfMovies = new List<Movie>();
            try
            {
                using var context = new DBContext();
                listOfMovies = context.Movie.Local.ToList();
            }
            catch
            {
                MessageBox.Show("Возникла ошибка при связи с базой данных!", "Ошибка!", MessageBoxButton.OK);
            }
            return listOfMovies;

        }
    }
}
