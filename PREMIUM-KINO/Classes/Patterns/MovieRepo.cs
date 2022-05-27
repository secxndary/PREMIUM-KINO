using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PREMIUM_KINO.EFCore;
using PREMIUM_KINO.EFCore.Entities;



namespace PREMIUM_KINO.Classes
{
    public class MovieRepo
    {
        private DBContext context;

        public MovieRepo() => context = new DBContext();



        public List<Movie> GetAllMovies()
        {
            try
            {
                context.Movie.Load();
                return context.Movie.Local.ToList();
            }
            catch
            {
                return new List<Movie>();
            }
        }



        public Movie GetMovie(Movie title)
        {
            try
            {
                var ret = context.Movie.FirstOrDefault(x => x.Title == title.Title);
                return ret;
            }
            catch
            {
                return null;
            }
        }



        public bool AddMovie(Movie movie)
        {
            try
            {
                context.Movie.Add(movie);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }



        public void DeleteMovie(Movie movie)
        {
            try
            {
                context.Remove(movie);
                context.SaveChanges();
            }
            catch { }
        }



        public bool DeleteMovieAndSchedule(Movie edit, Movie select)
        {
            try
            {
                var id = new SqlParameter("@id", edit.Id);
                context.Database.ExecuteSqlRaw("delete from SCHEDULE where ID_MOVIE = @id", id);
                context.Movie.Remove(select);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }



        public bool UpdateRating(UserOrder order, int rate)
        {
            try
            {
                context.Movie.Load();
                var title_movie = new SqlParameter("@title_movie", order.Title);
                var rating = new SqlParameter("@rating", rate);
                context.Database.ExecuteSqlRaw("update MOVIE set RATING = round((RATING * ((select count(*) from USERS) - 1) " +
                    " + @rating) / (select count(*) from USERS), 2) where TITLE = @title_movie", title_movie, rating);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
