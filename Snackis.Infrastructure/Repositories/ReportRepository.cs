using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Snackis.Domain.Entities;
using Snackis.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly MyDbContext _mydbcontext;

        public ReportRepository(MyDbContext mydbcontext)
        {
            _mydbcontext = mydbcontext;
        }
        public async Task<List<ApplicationReport>> GetAllAsync()
        {
            return await _mydbcontext.UserReports
                .Include(r => r.Post)
                .Include(r => r.Topic)
                .Include(r => r.Reporter)
                .ToListAsync();
        }
        public async Task<ApplicationReport?> GetByIdAsync(int id)
        {
            return await _mydbcontext.UserReports
                .Include(r => r.Post)
                .Include(r => r.Topic)
                .Include(r => r.Reporter)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task CreateAsync(ApplicationReport report)
        {
            _mydbcontext.UserReports.Add(report);
            await _mydbcontext.SaveChangesAsync();
        }
        public async Task UpdateAsync(ApplicationReport report)
        {
            _mydbcontext.UserReports.Update(report);
            await _mydbcontext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            await _mydbcontext.UserReports
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}
