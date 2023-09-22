using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace courses.Controllers
{
    [Authorize(Roles = "admin,courses editor,news editor,teacher")]
    public class AdministrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
