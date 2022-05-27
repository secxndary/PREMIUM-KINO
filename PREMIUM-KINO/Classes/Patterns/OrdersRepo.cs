using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PREMIUM_KINO.EFCore;
using PREMIUM_KINO.EFCore.Entities;



namespace PREMIUM_KINO.Classes
{
    public class OrdersRepo
    {
        private DBContext context;

        public OrdersRepo() => context = new DBContext();



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
