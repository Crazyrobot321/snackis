using Snackis.Domain.Entities;

namespace Snackis.Domain.Interfaces
{
    public interface ITopicRepository
    {
        Task CreateAsync(ApplicationTopic topic);
        Task DeleteAsync(int id);
        Task<List<ApplicationTopic>> GetAllAsync();
        Task<List<ApplicationTopic>> GetAllTopicsBySubId(int subCategoryId);
        Task<ApplicationTopic?> GetByIdAsync(int id);
        Task UpdateAsync(ApplicationTopic topic);
        Task<List<ApplicationPost>> GetPostsByTopicIdAsync(int id);
    }
}