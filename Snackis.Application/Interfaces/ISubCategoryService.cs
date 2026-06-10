using Snackis.Domain.Entities;

namespace Snackis.Application.Interfaces
{
    public interface ISubCategoryService
    {
        Task CreateAsync(ApplicationSubCategory subCategory);
        Task DeleteAsync(int id);
        Task UpdateAsync(ApplicationSubCategory subCategory);
        Task<List<ApplicationSubCategory>> GetAllAsync();
        Task<ApplicationSubCategory?> GetByIdAsync(int id);
        Task<List<ApplicationSubCategory>> GetByCategoryIdAsync(int categoryId);
    }
}