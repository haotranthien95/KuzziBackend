using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kuzzi.Models.Chat
{
    public class Message
    {
        [Required]
        public required string Id { get; set; }
        [Required]
        public required string LocalMessageId { get; set; }
        [Required]
        public required string SenderUserId { get; set; }
        [ForeignKey("SenderUserId")]
        public ChatUser? SenderUser { get; set; }
        public required string ConversationId { get; set; }
        [ForeignKey("ConversationId")]
        public Conversation? Conversation { get; set; }
        [Required]
        public required string Type { get; set; }

        public string? Value { get; set; }
        public string? Text { get; set; }
        [Required]
        public required string Status { get; set; }
        [Required]
        public required DateTime SentAt { get; set; }

        public  string? ReplyTo { get; set; }
        [ForeignKey("replyTo")]
        public Message? ReplyMessage { get; set; }

    }
}