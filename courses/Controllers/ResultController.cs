using Microsoft.AspNetCore.Mvc;

namespace courses.Controllers
{
    public class ResultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
