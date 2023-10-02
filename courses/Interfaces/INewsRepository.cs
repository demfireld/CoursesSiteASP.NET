using courses.Models;

namespace courses.Interfaces
{
    public interface INewsRepository
    {
        Task<IEnumerable<News>> GetAllAsync();
        Task<News> GetByIdAsync(int id);
        bool Add(News news);
        bool Update(News news);
        bool Delete(News news);
        bool Save();
    }
}
