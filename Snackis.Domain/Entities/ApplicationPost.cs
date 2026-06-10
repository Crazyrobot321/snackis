using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Xml.Linq;

namespace Snackis.Domain.Entities
{
    public class ApplicationPost
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int TopicId { get; set; }
        public ApplicationTopic Topic { get; set; } = null!;
        public string? UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }

        [NotMapped]
        public string AuthorName { get; set; } = "Anonym";
    }
}