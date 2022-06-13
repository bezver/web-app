using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Film
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public List<Country> Countries { get; set; }

        public List<Genre> Genres { get; set; }

        public List<Rating> Ratings { get; set; }

        [NotMapped]
        public double Rating { get; set; }
    }
}
