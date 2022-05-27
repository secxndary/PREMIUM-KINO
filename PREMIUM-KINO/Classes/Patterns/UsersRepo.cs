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
    public class UsersRepo
    {
        private DBContext context;

        public UsersRepo() => context = new DBContext();



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

    }
}
