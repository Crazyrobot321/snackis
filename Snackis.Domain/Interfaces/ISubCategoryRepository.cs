using Snackis.Domain.Entities;

namespace Snackis.Domain.Interfaces
{
    public interface ISubCategoryRepository
    {
        Task CreateAsync(ApplicationSubCategory subCategory);
        Task DeleteAsync(int id);
        Task UpdateAsync(ApplicationSubCategory subCategory);
        Task<List<ApplicationSubCategory>> GetAllAsync();
        Task<ApplicationSubCategory?> GetByIdAsync(int id);
        Task<List<ApplicationSubCategory>> GetByCategoryIdAsync(int categoryId);
    }
}