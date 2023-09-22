using courses.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace courses.DataBase
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Courses> Courses { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Staffs> Staffs { get; set; }
    }
}
