using HelpdeskApi.Dtos;
using HelpdeskApi.Interfaces;
using HelpdeskApi.Models;

namespace HelpdeskApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        public CategoryService(ICategoryRepository repo) => _repo = repo;

        public async Task<List<CategoryReadDto>> GetAllAsync()
        {
            var categories = await _repo.GetAllAsync();
            return categories.Select(c => new CategoryReadDto { Id = c.Id, Name = c.Name }).ToList();
        }

        public async Task<CategoryReadDto?> GetByIdAsync(int id)
        {
            var c = await _repo.GetByIdAsync(id);
            return c == null ? null : new CategoryReadDto { Id = c.Id, Name = c.Name };
        }

        public async Task<CategoryReadDto> CreateAsync(CreateCategoryDto dto)
        {
            var category = new Category { Name = dto.Name };
            await _repo.AddAsync(category);
            await _repo.SaveChangesAsync();
            return new CategoryReadDto { Id = category.Id, Name = category.Name };
        }

        public async Task<bool> UpdateAsync(int id, CreateCategoryDto dto)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) return false;
            category.Name = dto.Name;
            _repo.Update(category);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) return false;
            _repo.Delete(category);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}