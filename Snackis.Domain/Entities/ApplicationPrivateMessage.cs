using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Snackis.Domain.Entities
{
    public class ApplicationPrivateMessage
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public string SenderId { get; set; } = string.Empty;
        public ApplicationUser? Sender { get; set; }
        public string ReceiverId { get; set; } = string.Empty;
        public ApplicationUser? Receiver { get; set; }

        [NotMapped]
        public string SenderName { get; set; } = "Unknown";
    }
}
