using courses.Interfaces;
using courses.Models;
using courses.Repository;
using courses.ViewModels;
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
        private readonly UserManager<AppUser> _userManager;

        public AdministrationController(ICoursesRepository coursesRepository, ICategoriesRepository categoriesRepository, UserManager<AppUser> userManager)
        {
            _coursesRepository = coursesRepository;
            _categoriesRepository = categoriesRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AllUsers() => View(_userManager.Users.ToList());

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> AllCourses()
        {
            IEnumerable<Courses> courses = await _coursesRepository.GetAll();
            IEnumerable<Categories> categories = await _categoriesRepository.GetAll();

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
            IEnumerable<Categories> categories = await _categoriesRepository.GetAll();

            CreateCoursesViewModel createCoursesViewModel = new CreateCoursesViewModel()
            {
                Category = categories
            };
            return View(createCoursesViewModel);
        }

        [HttpPost]
        public IActionResult CreateCourse(Courses courses)
        {
            if (ModelState.IsValid)
            {
                return View(courses);
            }
            _coursesRepository.Add(courses);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin,courses editor")]
        [HttpGet]
        public async Task<IActionResult> EditCourse(int id)
        {
            IEnumerable<Categories> categories = await _categoriesRepository.GetAll();

            Courses course = await _coursesRepository.GetByIdAsync(id);
            if (course == null) { return View("Error"); }

            EditCourseViewModel EditCourseViewModel = new EditCourseViewModel
            {
                Name = course.Name,
                ShortDescription = course.ShortDescription,
                LongDescription = course.LongDescription,
                Img = course.Img,
                Price = course.Price,
                CategoryId = course.CategoryId,
                Category = categories
            };
            return View(EditCourseViewModel);
        }

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
                Name = editCourseViewModel.Name,
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
        public async Task<IActionResult> DeleteCourse(int id)
        {
            IEnumerable<Categories> categories = await _categoriesRepository.GetAll();

            Courses course = await _coursesRepository.GetByIdAsync(id);
            if (course == null) { return View("Error"); }

            EditCourseViewModel EditCourseViewModel = new EditCourseViewModel
            {
                Id = id,
                Name = course.Name,
                ShortDescription = course.ShortDescription,
                LongDescription = course.LongDescription,
                Img = course.Img,
                Price = course.Price,
                CategoryId = course.CategoryId,
                Category = categories
            };
            return View(course);
        }

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("DeleteCourses")]
        public async Task<IActionResult> DeleteCourses(int id)
        {
            Courses courses = await _coursesRepository.GetByIdAsync(id);
            if (courses == null) { return View("Error"); }

            _coursesRepository.Delete(courses);
            return RedirectToAction("AllCourses");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
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
                return View("Error", new string[] { "Пользователь не найден"});
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
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
    }
}
