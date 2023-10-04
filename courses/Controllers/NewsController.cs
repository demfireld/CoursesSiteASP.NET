using courses.Interfaces;
using courses.Models;
using courses.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace courses.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsRepository _newsRepository;

        public NewsController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<News> news = await _newsRepository.GetAllAsync();

            NewsViewModel newsViewModel = new NewsViewModel { News = news };
            return View(newsViewModel);
        }
    }
}
