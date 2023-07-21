using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace courses.Models
{
    public class AppUser : IdentityUser
    {
        public string UserSurname { get; set; }
        public string UserPatronymic { get; set; }
    }
}
