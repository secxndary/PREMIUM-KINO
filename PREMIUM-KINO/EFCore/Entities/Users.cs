using System;
using System.Collections.Generic;

namespace PREMIUM_KINO.EFCore.Entities
{
    public class Users
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Role { get; set; }
        public string Password { get; set; }

        public ICollection<Orders> Orders { get; set; }

        public Users() => Orders = new List<Orders>();
    }
}
