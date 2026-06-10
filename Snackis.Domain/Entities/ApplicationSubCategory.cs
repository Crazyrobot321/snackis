using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Snackis.Domain.Entities
{
    public class ApplicationSubCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public ApplicationCategory? Category { get; set; }

        // Navigation property
        public ICollection<ApplicationTopic> Topics { get; set; } = new List<ApplicationTopic>();
        public int TopicCount => Topics?.Count ?? 0;
    }
}