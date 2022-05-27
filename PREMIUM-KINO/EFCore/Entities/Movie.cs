using System;
using System.Collections.Generic;

namespace PREMIUM_KINO.EFCore.Entities
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; }
        public float Rating { get; set; }
        public string Photo { get; set; }

        public ICollection<Schedule> Schedule { get; set; }

        public Movie(string title, string director, string genre, int duration, float rating, string photo)
        {
            Schedule = new List<Schedule>();
            Id = Guid.NewGuid();
            Title = title;
            Director = director;
            Genre = genre;
            Duration = duration;
            Rating = rating;
            Photo = photo;
        }

        public override string ToString() => $"{Title} — {Director} ({Genre}) {Duration} мин; {Rating}";
    }
}
