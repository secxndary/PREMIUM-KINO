using System;

namespace PREMIUM_KINO.Classes
{
    public class Ticket
    {
        public static Ticket instance;

        public Guid Id_Schedule { get; set; }
        public DateTime DateTime { get; set; }
        public string Schedule { get => DateTime.ToLongDateString() + "\n      " + DateTime.ToShortTimeString(); }
        public string DateOfMovie { get => DateTime.ToLongDateString() + " " + DateTime.ToShortTimeString(); }
        public int Aviable_Seats { get; set; }

        public Guid Id_Movie { get; set; }
        public string Title { get; set; }   
        public string Director { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; }
        public float Rating { get; set; }
        public string Photo { get; set; }

        public Ticket() { }

        public Ticket(Guid id_sched, Guid id_movie, DateTime date, int aviable, string title, string director, string genre, int duration, float rating, string photo)
        {
            Id_Schedule = id_sched;
            Id_Movie = id_movie;
            DateTime = date;
            Aviable_Seats = aviable;
            Title = title;
            Director = director;
            Genre = genre;
            Duration = duration;
            Rating = rating;
            Photo = photo;
        }

        public override string ToString() => $"{DateTime.ToLongDateString()}";

        public static Ticket getInstance()
        {
            if (instance == null)
                instance = new Ticket();
            return instance; ;
        }

        public static Ticket getInstanceFull(Guid id_sched, Guid id_movie, DateTime date, int aviable, string title, string director, string genre, int duration, float rating, string photo)
        {
            if (instance == null)
                instance = new Ticket(id_sched, id_movie, date, aviable, title, director, genre, duration, rating, photo);
            return instance; ;
        }
    }
}
