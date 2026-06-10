using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Snackis.Domain.Entities
{
    public class ApplicationReport
    {
        public int Id { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime ReportedAt { get; set; } = DateTime.UtcNow;
        public int? PostId { get; set; }
        public ApplicationPost? Post { get; set; }
        public int? TopicId { get; set; }
        public ApplicationTopic? Topic { get; set; }
        public string? ReporterUserId { get; set; }
        public ApplicationUser? Reporter { get; set; }
        public bool IsResolved { get; set; } = false;

        [NotMapped]
        public string Name { get; set; } = string.Empty;
    }
}
