using Snackis.Domain.Entities;

namespace Snackis.Infrastructure.Repositories
{
    public interface IReportRepository
    {
        Task CreateAsync(ApplicationReport report);
        Task DeleteAsync(int id);
        Task<List<ApplicationReport>> GetAllAsync();
        Task<ApplicationReport?> GetByIdAsync(int id);
        Task UpdateAsync(ApplicationReport report);
    }
}