using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.DTOs
{
    public class PrivateMessageDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
        public string SenderId { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
        public string ReceiverName { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
    }
    public class CreatePrivateMessageDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string SenderId { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
        public DateTime? SentAt { get; set; }
    }
}
