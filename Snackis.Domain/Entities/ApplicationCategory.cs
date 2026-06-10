using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Snackis.Domain.Entities
{
    public class ApplicationCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        // Add this navigation property
        public ICollection<ApplicationSubCategory> SubCategories { get; set; } = new List<ApplicationSubCategory>();
        public ICollection<ApplicationTopic> Topics { get; set; } = new List<ApplicationTopic>();
    }
}
