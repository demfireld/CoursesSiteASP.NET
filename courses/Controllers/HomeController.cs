using courses.DataBase;
using courses.Models;
using courses.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace courses.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<Courses> _courses;
        private readonly List<Teachers> _teachers;

        public HomeController(ApplicationDbContext context)
        {
            _teachers = context.Teachers.ToList();
            _courses = context.Courses.ToList();
        }
        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel { Courses = _courses, Teachers = _teachers };
            return View(homeViewModel);
        }
    }
}
