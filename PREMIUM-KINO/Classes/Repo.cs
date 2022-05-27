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
    public class Repo 
    {
        private DBContext context;

        public Repo() => context = new DBContext();



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
            catch
            {
            }
        }




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


        public Users GetUserByLogin(string login)
        {
            try
            {
                using var context = new DBContext();
                var param = new SqlParameter("@login", login);
                List<Users> users = context.Users.FromSqlRaw("select * from Users where Login = @login", param).ToList();
                var user = users.FirstOrDefault(x => x.Login == login);
                return user;
            }
            catch
            {
                return new Users();
            }
        }



        public bool AddNewUser(Guid idd, string namee, string surrname, string loginn, string pass, string emaill, string phonee)
        {
            try
            {
                var id = new SqlParameter("@id", idd);
                var name = new SqlParameter("@name", namee);
                var surname = new SqlParameter("@surname", surrname);
                var login = new SqlParameter("@login", loginn);
                var password = new SqlParameter("@password", pass);
                var role = new SqlParameter("@role", 1);
                var email = new SqlParameter("@email", emaill);
                var phone = new SqlParameter("@phone", phonee);
                using var context = new DBContext();
                context.Database.ExecuteSqlRaw("insert into USERS (ID, NAME, SURNAME, LOGIN, PASSWORD, ROLE, EMAIL, PHONE) " +
                                               "values (@id, @name, @surname, @login, @password, @role, @email, @phone)",
                                                id, name, surname, login, password, role, email, phone);
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



        public bool AddOrder(Guid idd, Ticket schedulee, Users userr, int countt)
        {
            try
            {
                var id = new SqlParameter("@id", idd);
                var id_schedule = new SqlParameter("@id_schedule", schedulee.Id_Schedule);
                var id_user = new SqlParameter("@id_user", userr.Id);
                var count = new SqlParameter("@count", countt);

                context.Orders.Load();

                context.Database.ExecuteSqlRaw("insert into ORDERS (ID, ID_USER, ID_SCHEDULE, NUMBER_OF_SEATS, ORDER_STATUS) " +
                                               "values (@id, @id_user, @id_schedule, @count, 'Забронировано')",
                                                id, id_user, id_schedule, count);
                context.Database.ExecuteSqlRaw("update SCHEDULE set AVIABLE_SEATS = AVIABLE_SEATS - @count where ID = @id_schedule",
                                                count, id_schedule);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }



        public List<UserOrder> GetUserOrders(Users userSignedIn)
        {
            try
            {
                var res = from order in context.Orders
                          join schedule in context.Schedule on order.Id_Schedule equals schedule.Id
                          join movie in context.Movie on schedule.Id_Movie equals movie.Id
                          where order.Id_User == userSignedIn.Id
                          orderby schedule.DateTime
                          select new UserOrder
                          {
                              Title = movie.Title,
                              DateTime = schedule.DateTime,
                              Count_Of_Seats = order.Number_Of_Seats,
                              Order_Status = order.Order_Status,
                              Id_Schedule = schedule.Id
                          };
                var listOfUserOrders = res.Cast<UserOrder>().ToList();
                foreach (var order in listOfUserOrders)
                {
                    if (order.DateTime < DateTime.Now)
                    {
                        var status = new SqlParameter(@"status", "Просмотрено");
                        var id_schedule = new SqlParameter(@"id_schedule", order.Id_Schedule);
                        context.Database.ExecuteSqlRaw("update ORDERS set ORDER_STATUS = @status where ID_SCHEDULE = @id_schedule", status, id_schedule);
                        context.SaveChanges();
                    }
                    else
                    {
                        var status = new SqlParameter(@"status", "Забронировано");
                        var id_schedule = new SqlParameter(@"id_schedule", order.Id_Schedule);
                        context.Database.ExecuteSqlRaw("update ORDERS set ORDER_STATUS = @status where ID_SCHEDULE = @id_schedule", status, id_schedule);
                        context.SaveChanges();
                    }
                }
                var resNew = from order in context.Orders
                             join schedule in context.Schedule on order.Id_Schedule equals schedule.Id
                             join movie in context.Movie on schedule.Id_Movie equals movie.Id
                             where order.Id_User == userSignedIn.Id
                             orderby schedule.DateTime
                             select new UserOrder
                             {
                                 Title = movie.Title,
                                 DateTime = schedule.DateTime,
                                 Count_Of_Seats = order.Number_Of_Seats,
                                 Order_Status = order.Order_Status,
                                 Id_Schedule = schedule.Id
                             };
                var listOfUserOrdersNew = res.Cast<UserOrder>().ToList();
                return listOfUserOrdersNew;
            }
            catch
            {
                return new List<UserOrder>();
            }
        }





        public List<UserOrder> GetAllOrders(Users userSignedIn)
        {
            try
            {
                var res = from order in context.Orders
                          join schedule in context.Schedule on order.Id_Schedule equals schedule.Id
                          join movie in context.Movie on schedule.Id_Movie equals movie.Id
                          join user in context.Users on order.Id_User equals user.Id
                          orderby schedule.DateTime
                          select new UserOrder
                          {
                              Title = movie.Title,
                              DateTime = schedule.DateTime,
                              Count_Of_Seats = order.Number_Of_Seats,
                              Order_Status = order.Order_Status,
                              User_Name = user.Login,
                              Id_Schedule = schedule.Id
                          };
                var listOfUserOrders = res.Cast<UserOrder>().ToList();
                foreach (var order in listOfUserOrders)
                {
                    if (order.DateTime < DateTime.Now)
                    {
                        var status = new SqlParameter(@"status", "Просмотрено");
                        var id_schedule = new SqlParameter(@"id_schedule", order.Id_Schedule);
                        context.Database.ExecuteSqlRaw("update ORDERS set ORDER_STATUS = @status where ID_SCHEDULE = @id_schedule", status, id_schedule);
                        context.SaveChanges();
                    }
                    else
                    {
                        var status = new SqlParameter(@"status", "Забронировано");
                        var id_schedule = new SqlParameter(@"id_schedule", order.Id_Schedule);
                        context.Database.ExecuteSqlRaw("update ORDERS set ORDER_STATUS = @status where ID_SCHEDULE = @id_schedule", status, id_schedule);
                        context.SaveChanges();
                    }
                }

                var resNew = from order in context.Orders
                             join schedule in context.Schedule on order.Id_Schedule equals schedule.Id
                             join movie in context.Movie on schedule.Id_Movie equals movie.Id
                             join user in context.Users on order.Id_User equals user.Id
                             orderby schedule.DateTime
                             select new UserOrder
                             {
                                 Title = movie.Title,
                                 DateTime = schedule.DateTime,
                                 Count_Of_Seats = order.Number_Of_Seats,
                                 Order_Status = order.Order_Status,
                                 User_Name = user.Login,
                                 Id_Schedule = schedule.Id
                             };
                var listOfUserOrdersNew = resNew.Cast<UserOrder>().ToList();
                return listOfUserOrdersNew;
            }
            catch
            {
                return new List<UserOrder>();
            }
        }
    }
}
