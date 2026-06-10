using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int TopicId { get; set; }

    }
    public class CreatePostDTO
    {
        public string Content { get; set; } = string.Empty;
        public int TopicId { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
