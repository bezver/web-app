using System;

namespace Core.Models
{
    public class Rating
    {
        public string PersonId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public Guid FilmId { get; set; }
        public Film Film { get; set; }

        public int Point { get; set; }
    }
}
