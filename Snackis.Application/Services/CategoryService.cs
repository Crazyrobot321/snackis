using Snackis.Domain.Interfaces;
using Snackis.Domain.Entities;
using Snackis.Application.Interfaces;

namespace Snackis.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task CreateCategoryAsync(ApplicationCategory category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            if (string.IsNullOrWhiteSpace(category.Name))
                throw new ArgumentException("Category name cannot be empty");

            await _categoryRepository.CreateAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid category ID");

            await _categoryRepository.DeleteAsync(id);
        }

        public async Task UpdateCategoryAsync(ApplicationCategory category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            if (category.Id <= 0)
                throw new ArgumentException("Invalid category ID");

            if (string.IsNullOrWhiteSpace(category.Name))
                throw new ArgumentException("Category name cannot be empty");

            await _categoryRepository.UpdateAsync(category);
        }

        public async Task<List<ApplicationCategory>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAll();
        }

        public async Task<ApplicationCategory?> GetCategoryByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException($"Invalid category ID: {id}");

            return await _categoryRepository.GetByIdAsync(id);
        }
    }
}