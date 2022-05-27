using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.Input;
using PREMIUM_KINO.EFCore.Entities;

namespace PREMIUM_KINO
{
    public class MainFrameViewModel
    {
        public ObservableCollection<Movie> movies { get; set; }

        private static List<Movie> listStatic;

        public MainFrameViewModel()
        {
            movies = new ObservableCollection<Movie>(listStatic);
        }

        public static void getInfo()
        {
            listStatic = MovieContext.getAllMovies();
        }
    }
}
