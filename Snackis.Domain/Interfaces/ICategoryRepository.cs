using Snackis.Domain.Entities;

namespace Snackis.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task CreateAsync(ApplicationCategory category);
        Task DeleteAsync(int id);
        Task<List<ApplicationCategory>> GetAll();
        Task<ApplicationCategory?> GetByIdAsync(int id);
        Task UpdateAsync(ApplicationCategory category);
    }
}