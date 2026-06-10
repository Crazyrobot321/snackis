using Snackis.Domain.Interfaces;
using Snackis.Domain.Entities;
using Snackis.Application.Interfaces;

namespace Snackis.Application.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;

        public TopicService(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task CreateTopicAsync(ApplicationTopic topic)
        {
            if (topic == null)
                throw new ArgumentNullException(nameof(topic));
            if (string.IsNullOrWhiteSpace(topic.Title))
                throw new ArgumentException("Topic title cannot be empty");
            if (string.IsNullOrWhiteSpace(topic.AuthorId))
                throw new ArgumentException("AuthorId is required");

            await _topicRepository.CreateAsync(topic);
        }

        public async Task DeleteTopicAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid topic ID");

            await _topicRepository.DeleteAsync(id);
        }

        public async Task UpdateTopicAsync(ApplicationTopic topic)
        {
            if (topic == null)
                throw new ArgumentNullException(nameof(topic));
            if (string.IsNullOrWhiteSpace(topic.Title))
                throw new ArgumentException("Topic title cannot be empty");

            await _topicRepository.UpdateAsync(topic);
        }

        public async Task<ApplicationTopic?> GetTopicByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException($"Invalid topic ID: {id}");

            return await _topicRepository.GetByIdAsync(id);
        }

        public async Task<List<ApplicationTopic>> GetAllTopicsAsync()
        {
            return await _topicRepository.GetAllAsync();
        }

        public async Task<List<ApplicationTopic>> GetAllTopicsBySubId(int subCategoryId)
        {
            if (subCategoryId <= 0)
                throw new ArgumentException($"Invalid subcategory ID: {subCategoryId}");

            return await _topicRepository.GetAllTopicsBySubId(subCategoryId);
        }

        public async Task<List<ApplicationPost>> GetPostsByTopicIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid topic ID");

            return await _topicRepository.GetPostsByTopicIdAsync(id);
        }
    }
}