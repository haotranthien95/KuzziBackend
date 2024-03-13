using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KuzziMain.Areas.Api.Models.Conversation
{
    public class ConversationRequest
    {
        public required ICollection<string> ChatUserId { get; set; }
        public required ICollection<string> UserId { get; set; }
        
        public string? ConversationType { get; set; }
    }
}