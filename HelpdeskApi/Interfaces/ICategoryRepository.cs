using HelpdeskApi.Models;

namespace HelpdeskApi.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task AddAsync(Category category);
        void Update(Category category);
        void Delete(Category category);
        Task<bool> ExistsAsync(int id);
        Task SaveChangesAsync();
    }
}