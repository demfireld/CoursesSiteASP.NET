using Microsoft.AspNetCore.Mvc;

namespace courses.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
