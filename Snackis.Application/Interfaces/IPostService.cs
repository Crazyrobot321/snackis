using Snackis.Domain.Entities;

namespace Snackis.Application.Interfaces
{
    public interface IPostService
    {
        Task CreatePostAsync(ApplicationPost post);
        Task DeletePostAsync(int id);
        Task<ApplicationPost?> GetPostByIdAsync(int id);
        Task UpdatePostAsync(ApplicationPost post);
        Task<List<ApplicationPost>> GetAllPostsAsync();
        Task<List<ApplicationPost>> GetAllPostsByTopicAsync(int id);

    }
}