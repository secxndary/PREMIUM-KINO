using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PREMIUM_KINO.EFCore;
using PREMIUM_KINO.EFCore.Entities;



namespace PREMIUM_KINO.Classes
{
    public class ScheduleRepo
    {
        private DBContext context;

        public ScheduleRepo() => context = new DBContext();




        public bool AddSchedule(Guid idd, Guid idd_movie, int seatss, DateTime date)
        {
            try
            {
                var id = new SqlParameter("@id", idd);
                var id_movie = new SqlParameter("@id_movie", idd_movie);
                var seats = new SqlParameter("@seats", seatss);
                var datetime = new SqlParameter("@datetime", date);

                context.Database.ExecuteSqlRaw("insert into SCHEDULE (ID, ID_MOVIE, AVIABLE_SEATS, DATETIME) " +
                    "values (@id, @id_movie, @seats, @datetime)", id, id_movie, seats, datetime);

                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }




        public IEnumerable<Ticket> GetMovieTickets(Movie selectedFilm)
        {
            try
            {
                context.Schedule.Load();
                var tickets = context.Schedule.Local.Where(x => x.Id_Movie == selectedFilm.Id).OrderBy(x => x.DateTime).ToList().Select(i => new Ticket
                {
                    Id_Schedule = i.Id,
                    Id_Movie = selectedFilm.Id,
                    Title = selectedFilm.Title,
                    Aviable_Seats = i.Aviable_Seats,
                    Director = selectedFilm.Director,
                    Genre = selectedFilm.Genre,
                    Duration = selectedFilm.Duration,
                    Rating = selectedFilm.Rating,
                    DateTime = i.DateTime,
                    Photo = selectedFilm.Photo
                });
                return tickets;
            }
            catch
            {
                return null;
            }
        }
    }
}
