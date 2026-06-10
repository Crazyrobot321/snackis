using Snackis.Domain.Interfaces;
using Snackis.Domain.Entities;
using Snackis.Application.Interfaces;

namespace Snackis.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostReposistory _postReposistory;

        public PostService(IPostReposistory postReposistory)
        {
            _postReposistory = postReposistory;
        }

        public async Task CreatePostAsync(ApplicationPost post)
        {
            if (post == null)
                throw new ArgumentNullException(nameof(post));
            if (string.IsNullOrWhiteSpace(post.Content))
                throw new ArgumentException("Post content cannot be empty");
            if (post.TopicId <= 0)
                throw new ArgumentException("Invalid topic ID");

            await _postReposistory.CreateAsync(post);
        }

        public async Task DeletePostAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid post ID");

            await _postReposistory.DeleteAsync(id);
        }

        public async Task UpdatePostAsync(ApplicationPost post)
        {
            if (post == null)
                throw new ArgumentNullException(nameof(post));
            if (post.Id <= 0)
                throw new ArgumentException("Invalid post ID");
            if (string.IsNullOrWhiteSpace(post.Content))
                throw new ArgumentException("Post content cannot be empty");

            await _postReposistory.UpdateAsync(post);
        }

        public async Task<ApplicationPost?> GetPostByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException($"Invalid post ID: {id}");

            return await _postReposistory.GetByIdAsync(id);
        }

        public async Task<List<ApplicationPost>> GetAllPostsAsync()
        {
            return await _postReposistory.GetAllAsync();
        }

        public async Task<List<ApplicationPost>> GetAllPostsByTopicAsync(int id)
        {
            return await _postReposistory.GetAllByTopicAsync(id);
        }
    }
}