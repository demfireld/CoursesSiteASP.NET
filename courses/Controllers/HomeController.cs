using courses.Models;
using courses.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace courses.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<Courses> _courses;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
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

        public async Task<IActionResult> Detail(string Id)
        {
            AppUser user = await _userManager.FindByNameAsync(Id);

            ProfileUserViewModel detailUserViewModel = new ProfileUserViewModel()
            {
                Id = Id,
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return View(detailUserViewModel);
        }
    }
}
