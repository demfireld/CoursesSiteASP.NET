using Microsoft.AspNetCore.Mvc;

namespace courses.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
