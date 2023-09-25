using courses.DataBase;
using courses.Interfaces;
using courses.Models;
using Microsoft.EntityFrameworkCore;

namespace courses.Repository
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categories>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        //public async Task<Categories> GetByIdAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        public bool Add(Categories categories)
        {
            _context.Categories.Add(categories);
            return Save();
        }

        public bool Delete(Categories categories)
        {
            _context.Categories.Remove(categories);
            return Save();
        }

        public bool Save()
        {
            var isSaved = _context.SaveChanges();
            return isSaved > 0 ? true : false;
        }

        public bool Update(Categories categories)
        {
            _context.Update(categories);
            return Save();
        }
    }
}
