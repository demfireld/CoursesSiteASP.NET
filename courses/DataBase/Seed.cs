using courses.Models;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace courses.DataBase
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(new List<Categories>()
                    {
                        new Categories()
                        {
                            CategoryName = "Куртки"
                        },
                        new Categories()
                        {
                            CategoryName = "Шапки"
                        },
                        new Categories()
                        {
                            CategoryName = "Балаклавы"
                        }
                    });
                    context.SaveChanges();
                }
                if (!context.Courses.Any())
                {
                    context.Courses.AddRange(new List<Courses>()
                    {
                        new Courses()
                        {
                            Name = "Зимняя куртка",
                            ShortDescription = "Короткое описание",
                            LongDescription = "Длинное описание",
                            Img = "путь до картинки",
                            Price = 99.99,
                            CategoryId = 1
                        },

                        new Courses()
                        {
                            Name = "Балаклава в виде летней расцветки",
                            ShortDescription = "Короткое описание",
                            LongDescription = "Длинное описание",
                            Img = "путь до картинки",
                            Price = 342.99,
                            CategoryId = 3
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "gamew5177@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "Nazar",
                        UserSurname = "Kirilov",
                        UserPatronymic = "Andreevich",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Maksim12088!");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }
            }
        }
    }
}
