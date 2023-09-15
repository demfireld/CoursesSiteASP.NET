using courses.DataBase;
using courses.Interfaces;
using courses.Models;
using courses.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace courses.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        //private readonly List<Categories> _categories;

        public CoursesController(ICoursesRepository coursesRepository, ICategoriesRepository categoriesRepository)
        {
            _coursesRepository = coursesRepository;
            _categoriesRepository = categoriesRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Courses> courses = await _coursesRepository.GetAll();
            IEnumerable<Categories> categories = await _categoriesRepository.GetAll();

            PriceViewModel priceViewModel = new PriceViewModel { Courses = courses, Categories = categories};
            return View(priceViewModel);

            //PriceViewModel priceViewModel = new PriceViewModel { Courses = _courses , Categories = _categories };
            //return View(priceViewModel);
        }

        //public IActionResult Create()
        //{
        //    return View();
        //}
    }
}
