using Microsoft.EntityFrameworkCore;
using Snackis.Domain.Entities;
using Snackis.Infrastructure.Data;
using Snackis.Domain.Interfaces;

namespace Snackis.Infrastructure.Repositories
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly MyDbContext _myDbContext;

        public SubCategoryRepository(MyDbContext mydbcontext)
        {
            _myDbContext = mydbcontext;
        }

        public async Task CreateAsync(ApplicationSubCategory subCategory)
        {
            _myDbContext.SubCategories.Add(subCategory);
            await _myDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var subCategory = await _myDbContext.SubCategories
                .Include(sc => sc.Topics)
                    .ThenInclude(t => t.Posts)
                .FirstOrDefaultAsync(sc => sc.Id == id);

            if (subCategory != null)
            {
                _myDbContext.SubCategories.Remove(subCategory);
                await _myDbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(ApplicationSubCategory subCategory)
        {
            _myDbContext.SubCategories.Update(subCategory);
            await _myDbContext.SaveChangesAsync();
        }

        public async Task<List<ApplicationSubCategory>> GetAllAsync()
        {
            return await _myDbContext.SubCategories
                .Include(sc => sc.Category)
                .Include(sc => sc.Topics)
                    .ThenInclude(sc => sc.Posts)
                .ToListAsync();
        }

        public async Task<ApplicationSubCategory?> GetByIdAsync(int id)
        {
            return await _myDbContext.SubCategories
                .Include(sc => sc.Category)
                .Include(sc => sc.Topics)
                .FirstOrDefaultAsync(sc => sc.Id == id);
        }

        public async Task<List<ApplicationSubCategory>> GetByCategoryIdAsync(int categoryId)
        {
            return await _myDbContext.SubCategories
                .Where(sc => sc.CategoryId == categoryId)
                .Include(sc => sc.Topics)
                .ToListAsync();
        }
    }
}