using Snackis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.Interfaces.Api
{
    public interface ICategoryServiceApi
    {
        Task CreateCategoryAsync(ApplicationCategory category);
        Task DeleteCategoryAsync(int id);
        Task<List<ApplicationCategory>> GetAllCategoriesAsync();
        Task<ApplicationCategory?> GetCategoryByIdAsync(int id);
        Task UpdateCategoryAsync(ApplicationCategory category);
    }
}
