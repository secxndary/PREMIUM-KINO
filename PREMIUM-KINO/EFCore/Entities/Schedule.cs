using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PREMIUM_KINO.EFCore.Entities
{
    public class Schedule
    {
        public Guid Id { get; set; }
        public Guid Id_Movie { get; set; }
        public DateTime DateTime { get; set; }
        public int Aviable_Seats { get; set; }

        [NotMapped]
        public Movie Movie { get; set; }
    }
}
