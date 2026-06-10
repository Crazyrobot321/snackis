using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Snackis.Domain.Entities
{
    public class ApplicationTopic
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public string AuthorId { get; set; } = string.Empty;
        public DateTime? Created { get; set; }
        public string? ImageSource { get; set; }
        public ApplicationCategory? Category { get; set; }
        public ApplicationSubCategory? SubCategory { get; set; }
        public ApplicationUser? Author { get; set; }
        public List<ApplicationPost> Posts { get; set; } = new();
        [NotMapped] //Do not create a column for this property in the database
        public string AuthorName { get; set; } = "Anonym";
        [NotMapped]
        public int PostCount { get; set; }
    }
}