using Snackis.Domain.Entities;

namespace Snackis.Application.Interfaces
{
    public interface IReportService
    {
        Task CreateReportAsync(ApplicationReport report);
        Task DeleteReportAsync(int id);
        Task<List<ApplicationReport>> GetAllReportsAsync();
        Task<ApplicationReport?> GetReportByIdAsync(int id);
        Task UpdateReportAsync(ApplicationReport report);
    }
}