using courses.Models;

namespace courses.Interfaces
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Categories>> GetAllAsync();
       // Task<Categories> GetByIdAsync(int id);

        bool Add(Categories categories);
        bool Update(Categories categories);
        bool Delete(Categories categories);
        bool Save();
    }
}
