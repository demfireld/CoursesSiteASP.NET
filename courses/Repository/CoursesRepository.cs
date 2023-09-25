using courses.DataBase;
using courses.Interfaces;
using courses.Models;
using Microsoft.EntityFrameworkCore;

namespace courses.Repository
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly ApplicationDbContext _context;

        public CoursesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Courses>> GetAll()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Courses> GetByIdAsync(int id)
        {
            return await _context.Courses.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Add(Courses courses)
        {
            _context.Courses.Add(courses);
            return Save();
        }

        public bool Delete(Courses courses)
        {
            _context.Courses.Remove(courses);
            return Save();
        }

        public bool Save()
        {
            var isSaved = _context.SaveChanges();
            return isSaved > 0 ? true : false;
        }

        public bool Update(Courses courses)
        {
            _context.Update(courses);
            return Save();
        }
    }
}
