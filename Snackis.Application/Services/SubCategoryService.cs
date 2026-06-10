using Snackis.Domain.Interfaces;
using Snackis.Domain.Entities;
using Snackis.Application.Interfaces;

namespace Snackis.Application.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _repository;

        public SubCategoryService(ISubCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(ApplicationSubCategory subCategory)
        {
            if (subCategory == null)
                throw new ArgumentNullException(nameof(subCategory));
            if (string.IsNullOrWhiteSpace(subCategory.Name))
                throw new ArgumentException("SubCategory name cannot be empty");
            if (subCategory.CategoryId <= 0)
                throw new ArgumentException("Invalid category ID");

            await _repository.CreateAsync(subCategory);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid subcategory ID");

            await _repository.DeleteAsync(id);
        }

        public async Task UpdateAsync(ApplicationSubCategory subCategory)
        {
            if (subCategory == null)
                throw new ArgumentNullException(nameof(subCategory));
            if (subCategory.Id <= 0)
                throw new ArgumentException("Invalid subcategory ID");
            if (string.IsNullOrWhiteSpace(subCategory.Name))
                throw new ArgumentException("SubCategory name cannot be empty");
            if (subCategory.CategoryId <= 0)
                throw new ArgumentException("Invalid category ID");

            await _repository.UpdateAsync(subCategory);
        }

        public async Task<List<ApplicationSubCategory>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ApplicationSubCategory?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid subcategory ID");

            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<ApplicationSubCategory>> GetByCategoryIdAsync(int categoryId)
        {
            if (categoryId <= 0)
                throw new ArgumentException("Invalid category ID");

            return await _repository.GetByCategoryIdAsync(categoryId);
        }
    }
}