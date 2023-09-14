using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace courses.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
    }
}
