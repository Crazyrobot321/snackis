using Snackis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.Interfaces.Api
{
    public interface ISubCategoryServiceApi
    {
        Task CreateAsync(ApplicationSubCategory subCategory);
        Task DeleteAsync(int id);
        Task UpdateAsync(ApplicationSubCategory subCategory);
        Task<List<ApplicationSubCategory>> GetAllAsync();
        Task<ApplicationSubCategory?> GetByIdAsync(int id);
        Task<List<ApplicationSubCategory>> GetByCategoryIdAsync(int categoryId);
    }
}
