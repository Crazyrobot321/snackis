using Snackis.Domain.Entities;

namespace Snackis.Application.Interfaces
{
    public interface ITopicService
    {
        Task CreateTopicAsync(ApplicationTopic topic);
        Task DeleteTopicAsync(int id);
        Task<ApplicationTopic?> GetTopicByIdAsync(int id);
        Task UpdateTopicAsync(ApplicationTopic topic);
        Task<List<ApplicationTopic>> GetAllTopicsAsync();
        Task<List<ApplicationTopic>> GetAllTopicsBySubId(int subCategoryId);
        Task<List<ApplicationPost>> GetPostsByTopicIdAsync(int id);
    }
}