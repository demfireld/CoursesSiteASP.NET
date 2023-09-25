using courses.Interfaces;
using courses.Models;
using courses.Repository;
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
        public IActionResult AllUsers() => View(_userManager.Users.ToList());

        //public async IActionResult AllCourses()
        //{
        //    IEnumerable<Courses> courses = await _coursesRepository.GetAll();
        //    IEnumerable<Categories> categories = await _categoriesRepository.GetAll();


        //}
    }
}
