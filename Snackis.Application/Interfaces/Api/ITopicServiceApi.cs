using Snackis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.Interfaces.Api
{
    public interface ITopicServiceApi
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
