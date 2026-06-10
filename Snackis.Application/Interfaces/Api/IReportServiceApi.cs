using Snackis.Domain.Entities;

namespace Snackis.Application.Interfaces.Api
{
    public interface IReportServiceApi
    {
        Task CreateReportAsync(ApplicationReport report);
        Task DeleteReportAsync(int id);
        Task<List<ApplicationReport>> GetAllReportsAsync();
        Task<ApplicationReport?> GetReportByIdAsync(int id);
        Task UpdateReportAsync(ApplicationReport report);
    }
}