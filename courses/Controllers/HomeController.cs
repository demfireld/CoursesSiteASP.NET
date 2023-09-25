using courses.DataBase;
using courses.Models;
using courses.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace courses.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<Courses> _courses;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel { Courses = _courses };
            return View(homeViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
