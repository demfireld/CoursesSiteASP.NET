using courses.Models;

namespace courses.Interfaces
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Categories>> GetAll();
       // Task<Categories> GetByIdAsync(int id);

        bool Add(Categories categories);
        bool Update(Categories categories);
        bool Delete(Categories categories);
        bool Save();
    }
}
