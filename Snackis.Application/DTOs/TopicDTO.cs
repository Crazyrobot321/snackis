using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.DTOs
{
    public class TopicDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public DateTime? Created { get; set; }
        public int? SubCategoryId { get; set; }
        public int PostCount { get; set; }
        public string? ImageSource { get; set; }
    }
    public class CreateTopicDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string AuthorId { get; set; } = string.Empty;
        public DateTime? Created { get; set; }
        public int? SubCategoryId { get; set; }
        public string? ImageSource { get; set; }
    }
}
