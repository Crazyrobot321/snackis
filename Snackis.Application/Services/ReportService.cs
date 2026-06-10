using Snackis.Application.Interfaces;
using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;
using Snackis.Infrastructure.Repositories;

namespace Snackis.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task CreateReportAsync(ApplicationReport report)
        {

            if (report == null)
                throw new ArgumentNullException(nameof(report));
            await _reportRepository.CreateAsync(report);
        }

        public async Task DeleteReportAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid id");

            await _reportRepository.DeleteAsync(id);
        }

        public async Task UpdateReportAsync(ApplicationReport report)
        {
            if (report == null)
                throw new ArgumentNullException(nameof(report));
            if (report.Id <= 0)
                throw new ArgumentException("Invalid report Id");
            await _reportRepository.UpdateAsync(report);
        }

        public async Task<List<ApplicationReport>> GetAllReportsAsync()
        {
            return await _reportRepository.GetAllAsync();
        }

        public async Task<ApplicationReport?> GetReportByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Id");
            return await _reportRepository.GetByIdAsync(id);
        }
    }
}