using Microsoft.AspNetCore.Mvc;

namespace courses.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
