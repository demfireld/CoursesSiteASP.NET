using courses.Interfaces;
using courses.Models;
using courses.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace courses.Controllers
{
    [Authorize(Roles = "admin,courses editor,news editor,teacher")]
    public class AdministrationController : Controller
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly INewsRepository _newsRepository;
        
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AdministrationController(ICoursesRepository coursesRepository, 
                                        ICategoriesRepository categoriesRepository, 
                                        INewsRepository newsRepository, 
                                        RoleManager<IdentityRole> roleManager, 
                                        UserManager<AppUser> userManager)
        {
            _coursesRepository = coursesRepository;
            _categoriesRepository = categoriesRepository;
            _newsRepository = newsRepository;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Users
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AllUsers() => View(_userManager.Users.ToList());
        
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("AllUsers");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "Пользователь не найден" });
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            AppUser? user = await _userManager.FindByIdAsync(id);
            EditUserViewModel editUserViewModel = new EditUserViewModel()
            {
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                
            };

            if (user != null) { return View(editUserViewModel); }
            else { return RedirectToAction("AllUsers"); }

        }

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("EditUser")]
        public async Task<IActionResult> EditUser(string id, EditUserViewModel editUserViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByIdAsync(id);

                if (user != null)
                {
                    AppUser emailUser = await _userManager.FindByEmailAsync(editUserViewModel.Email);

                    if (user.Email == editUserViewModel.Email || emailUser == null)
                    {
                        user.Name = editUserViewModel.Name;
                        user.Surname = editUserViewModel.Surname;
                        if (editUserViewModel.Patronymic != null)
                        {
                            user.Patronymic = editUserViewModel.Patronymic;
                        }
                        else
                        {
                            user.Patronymic = null;
                        }
                        user.UserName = editUserViewModel.Email;
                        user.Email = editUserViewModel.Email;
                        user.PhoneNumber = editUserViewModel.PhoneNumber;

                        IdentityResult result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded) { return RedirectToAction("AllUsers"); }
                        else { return View(editUserViewModel); }
                    }
                }
                return RedirectToAction("AllUsers");
            }
            return View(editUserViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> EditRole(string userId)
        {
            // получаем пользователя
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditRole(string userId, List<string> roles)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = _roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }
        #endregion

        #region Courses
        [Authorize(Roles = "admin,courses editor")]
        [HttpGet]
        public async Task<IActionResult> AllCourses()
        {
            IEnumerable<Courses> courses = await _coursesRepository.GetAllAsync();
            IEnumerable<Categories> categories = await _categoriesRepository.GetAllAsync();

            AllCoursesViewModel allCoursesViewModel = new AllCoursesViewModel()
            {
                Courses = courses,
                Categories = categories
            };

            return View(allCoursesViewModel);
        }

        [Authorize(Roles = "admin,courses editor")]
        [HttpGet]
        public async Task<IActionResult> CreateCourse()
        {
            IEnumerable<Categories> categories = await _categoriesRepository.GetAllAsync();

            CreateCourseViewModel createCoursesViewModel = new CreateCourseViewModel()
            {
                Category = categories
            };
            return View(createCoursesViewModel);
        }

        [Authorize(Roles = "admin,courses editor")]
        [HttpPost]
        public IActionResult CreateCourse(Courses courses)
        {
            if (ModelState.IsValid)
            {
                return View(courses);
            }
            _coursesRepository.Add(courses);
            return RedirectToAction("AllCourses");
        }

        [Authorize(Roles = "admin,courses editor")]
        [HttpGet]
        public async Task<IActionResult> EditCourse(int id)
        {
            IEnumerable<Categories> categories = await _categoriesRepository.GetAllAsync();

            Courses course = await _coursesRepository.GetByIdAsync(id);
            if (course == null) { return View("Error"); }

            EditCourseViewModel editCourseViewModel = new EditCourseViewModel
            {
                Title = course.Title,
                ShortDescription = course.ShortDescription,
                LongDescription = course.LongDescription,
                Img = course.Img,
                Price = course.Price,
                CategoryId = course.CategoryId,
                Category = categories
            };
            return View(editCourseViewModel);
        }

        [Authorize(Roles = "admin,courses editor")]
        [HttpPost]
        public IActionResult EditCourse(int id, EditCourseViewModel editCourseViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ошибка редактирования курса");
                return View("EditCourse", editCourseViewModel);
            }

            Courses course = new Courses()
            {
                Id = id,
                Title = editCourseViewModel.Title,
                ShortDescription = editCourseViewModel.ShortDescription,
                LongDescription = editCourseViewModel.LongDescription,
                Img = editCourseViewModel.Img,
                Price = editCourseViewModel.Price,
                CategoryId = editCourseViewModel.CategoryId,
            };
            _coursesRepository.Update(course);
            return RedirectToAction("AllCourses");
        }

        [Authorize(Roles = "admin,courses editor")]
        [HttpGet]
        public async Task<IActionResult> DeleteCourses(int id)
        {
            IEnumerable<Categories> categories = await _categoriesRepository.GetAllAsync();

            Courses course = await _coursesRepository.GetByIdAsync(id);
            if (course == null) { return View("Error"); }

            EditCourseViewModel EditCourseViewModel = new EditCourseViewModel
            {
                Id = id,
                Title = course.Title,
                ShortDescription = course.ShortDescription,
                LongDescription = course.LongDescription,
                Img = course.Img,
                Price = course.Price,
                CategoryId = course.CategoryId,
                Category = categories
            };
            return View(course);
        }

        [Authorize(Roles = "admin,courses editor")]
        [HttpPost, ActionName("DeleteCourse")]
        public async Task<IActionResult> deleteCourses(int id)
        {
            Courses courses = await _coursesRepository.GetByIdAsync(id);
            if (courses == null) { return View("Error"); }

            _coursesRepository.Delete(courses);
            return RedirectToAction("AllCourses");
        }
        #endregion

        #region News
        [Authorize(Roles = "admin,news editor")]
        [HttpGet]
        public async Task<IActionResult> AllNews()
        {
            IEnumerable<News> news = await _newsRepository.GetAllAsync();

            AllNewsViewModel allNewsViewModel = new AllNewsViewModel()
            {
                News = news 
            };

            return View(allNewsViewModel);
        }

        [Authorize(Roles ="admin,news editor")]
        [HttpGet]
        public async Task<IActionResult> EditNewsAsync(int id)
        {
            News news = await _newsRepository.GetByIdAsync(id);
            if (news == null) { return View("Error"); }

            EditNewsViewModel editNewsViewModel = new EditNewsViewModel()
            {
                Title = news.Title,
                Description = news.Description
            };
            return View(editNewsViewModel);
        }

        [Authorize(Roles = "admin,news editor")]
        [HttpPost]
        public IActionResult EditNews(int id, EditNewsViewModel editNewsViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ошибка редактирования новости");
                return View("EditCourse", editNewsViewModel);
            }

            News news = new News()
            {
                Id = id,
                Title = editNewsViewModel.Title,
                Description = editNewsViewModel.Description,
            };
            _newsRepository.Update(news);
            return RedirectToAction("AllNews");
        }

        [Authorize(Roles = "admin,news editor")]
        [HttpGet]
        public async Task<IActionResult> CreateNews()
        {
            return View();
        }

        [Authorize(Roles = "admin,news editor")]
        [HttpPost]
        public IActionResult CreateNews(News news)
        {
            if (!ModelState.IsValid)
            {
                return View(news);
            }
            _newsRepository.Add(news);
            return RedirectToAction("AllNews");
        }
        #endregion
    }
}
