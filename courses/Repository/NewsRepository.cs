using courses.DataBase;
using courses.Interfaces;
using courses.Models;
using Microsoft.EntityFrameworkCore;

namespace courses.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly ApplicationDbContext _context;

        public NewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<News>> GetAllAsync()
        {
            return await _context.News.ToListAsync();
        }

        public async Task<News> GetByIdAsync(int id)
        {
            return await _context.News.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Add(News news)
        {
            _context.News.Add(news);
            return Save();
        }

        public bool Delete(News news)
        {
            _context.News.Remove(news);
            return Save();
        }


        public bool Save()
        {
            var isSaved = _context.SaveChanges();
            return isSaved > 0 ? true : false;
        }

        public bool Update(News news)
        {
            _context.Update(news);
            return Save();
        }
    }
}
