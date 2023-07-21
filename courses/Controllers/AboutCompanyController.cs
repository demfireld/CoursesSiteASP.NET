using Microsoft.AspNetCore.Mvc;

namespace courses.Controllers
{
    public class AboutCompanyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
