using courses.Models;
using courses.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace courses.Controllers
{
    public class ProfileUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public ProfileUserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string Id)
        {
            AppUser user = await _userManager.FindByIdAsync(Id);

            if (user != null)
            {
                ProfileUserViewModel viewModel = new ProfileUserViewModel()
                {
                    Id = Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Patronymic = user.Patronymic,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };
                return View(viewModel);
            }
            return NotFound();
        }
    }
}
