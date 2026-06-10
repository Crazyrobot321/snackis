using Snackis.Domain.Entities;

namespace Snackis.Application.Interfaces
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(ApplicationCategory category);
        Task DeleteCategoryAsync(int id);
        Task<List<ApplicationCategory>> GetAllCategoriesAsync();
        Task<ApplicationCategory?> GetCategoryByIdAsync(int id);
        Task UpdateCategoryAsync(ApplicationCategory category);
    }
}