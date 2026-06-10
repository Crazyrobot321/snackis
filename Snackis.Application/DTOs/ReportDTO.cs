using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.DTOs
{
    public class ReportDTO
    {
        public int Id { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime ReportedAt { get; set; }
        public int? PostId { get; set; }
        public int? TopicId { get; set; }
        public string ReporterUserId { get; set; } = string.Empty;
        public string ReporterUserName { get; set; } = string.Empty;
        public bool IsResolved { get; set; }
    }
    public class CreateReportDTO
    {
        public string Reason { get; set; } = string.Empty;
        public int? PostId { get; set; }
        public int? TopicId { get; set; }
        public string ReporterUserId { get; set; } = string.Empty;
    }
}
