using Snackis.Domain.Entities;

namespace Snackis.Domain.Interfaces
{
    public interface IPostReposistory
    {
        Task CreateAsync(ApplicationPost post);
        Task DeleteAsync(int id);
        Task<ApplicationPost?> GetByIdAsync(int id);
        Task UpdateAsync(ApplicationPost post);
        Task<List<ApplicationPost>> GetAllAsync();
        Task<List<ApplicationPost>> GetAllByTopicAsync(int id);
    }
}