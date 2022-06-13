using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Models
{
    public class Genre
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public List<Film> Films { get; set; }
    }
}
