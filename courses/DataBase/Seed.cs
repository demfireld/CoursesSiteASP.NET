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
                            CategoryName = "Зимние"
                        },
                        new Categories()
                        {
                            CategoryName = "Весенние"
                        },
                        new Categories()
                        {
                            CategoryName = "Летние"
                        },
                        new Categories()
                        {
                            CategoryName = "Осенние"
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
                            Name = "Молодой боец",
                            ShortDescription = "Короткое описание",
                            LongDescription = "Длинное описание",
                            Img = "путь до картинки",
                            Price = 99.99,
                            CategoryId = 3
                        },

                        new Courses()
                        {
                            Name = "Зимний летчик",
                            ShortDescription = "Короткое описание",
                            LongDescription = "Длинное описание",
                            Img = "путь до картинки",
                            Price = 342.99,
                            CategoryId = 1
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Teachers.Any())
                {
                    context.Teachers.AddRange(new List<Teachers>()
                    {
                        new Teachers()
                        {
                            TeacherName = "Валерий",
                            TeacherSurname = "Бережной",
                            TeacherPatronymic = "Владимирович",
                            TeacherAge = 35,
                            TeacherWorkExperience = 10,
                            TeacherPhoneNumber = "+71234567890",
                            TeacherAddress = "Москва",
                            TeacherImg = "fdggg"
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Staffs.Any())
                {
                    context.Staffs.AddRange(new List<Staffs>()
                    {
                        new Staffs()
                        {
                            StaffName = "Назари",
                            StaffSurname = "Кирилов",
                            StaffPatronymic = "Андреевич",
                            StaffAge = 19,
                            StaffWorkExperience = 1,
                            StaffPhoneNumber = "+71234567890",
                            StaffAddress = "Жуковский",
                            StaffImg = "fdfdf"
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
                        Name = "Nazar",
                        Surname = "Kirilov",
                        Patronymic = "Andreevich",
                        Email = adminUserEmail,
                        UserName = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Joker1234!");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }
            }
        }
    }
}
