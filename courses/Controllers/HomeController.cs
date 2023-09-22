using courses.DataBase;
using courses.Models;
using courses.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace courses.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<Courses> _courses;

        public HomeController(ApplicationDbContext context)
        {
            _courses = context.Courses.ToList();
        }
        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel { Courses = _courses };
            return View(homeViewModel);
        }
    }
}
