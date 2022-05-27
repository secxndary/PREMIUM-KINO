using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PREMIUM_KINO.EFCore.Entities
{
    public class Orders
    {
        public Guid Id { get; set; }
        public Guid Id_User { get; set; }
        public Guid Id_Schedule { get; set; }
        public int Number_Of_Seats { get; set; }
        public string Order_Status { get; set; }

        [NotMapped]
        public Users User { get; set; }

    }
}
