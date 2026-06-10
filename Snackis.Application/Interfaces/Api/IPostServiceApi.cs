using Snackis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.Interfaces.Api
{
    public interface IPostServiceApi
    {
        Task CreatePostAsync(ApplicationPost post);
        Task DeletePostAsync(int id);
        Task<ApplicationPost?> GetPostByIdAsync(int id);
        Task UpdatePostAsync(ApplicationPost post);
        Task<List<ApplicationPost>> GetAllPostsAsync(int id);
    }
}
