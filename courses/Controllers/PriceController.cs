using courses.DataBase;
using courses.Models;
using courses.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace courses.Controllers
{
    public class PriceController : Controller
    {
        private List<Courses> _courses;
        private List<Categories> _categories;

        public PriceController(ApplicationDbContext context)
        {
            _courses = context.Courses.ToList();
            _categories = context.Categories.ToList();
        }

        public IActionResult Index()
        {
            PriceViewModel priceViewModel = new PriceViewModel { Courses = _courses, Categories = _categories };
            return View(priceViewModel);
        }
    }
}
