using Microsoft.AspNetCore.Mvc;

namespace courses.Controllers
{
    public class ReviewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
