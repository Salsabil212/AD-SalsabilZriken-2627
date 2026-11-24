using Microsoft.EntityFrameworkCore;
using HelpdeskApi.Data;
using HelpdeskApi.Interfaces;
using HelpdeskApi.Models;

namespace HelpdeskApi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly HelpdeskDbContext _context;
        public CategoryRepository(HelpdeskDbContext context) => _context = context;

        public async Task<List<Category>> GetAllAsync() => await _context.Categories.ToListAsync();
        public async Task<Category?> GetByIdAsync(int id) => await _context.Categories.FindAsync(id);
        public async Task AddAsync(Category category) => await _context.Categories.AddAsync(category);
        public void Update(Category category) => _context.Categories.Update(category);
        public void Delete(Category category) => _context.Categories.Remove(category);
        public async Task<bool> ExistsAsync(int id) => await _context.Categories.AnyAsync(c => c.Id == id);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}