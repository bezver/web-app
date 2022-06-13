using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Rating> Ratings { get; set; }

        [NotMapped]
        public IEnumerable<string> Roles { get; set; }
    }
}
