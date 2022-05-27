using System;

namespace PREMIUM_KINO.Classes
{
    public class UserOrder
    {
        public static UserOrder instance;

        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public int Count_Of_Seats { get; set; }
        public string Order_Status { get; set; }
        public string User_Name { get; set; }
        public Guid Id_Schedule { get; set; }

        public static UserOrder getInstance()
        {
            if (instance == null)
                instance = new UserOrder();
            return instance; ;
        }
    }
}
