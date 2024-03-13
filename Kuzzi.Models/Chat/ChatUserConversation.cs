using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuzzi.Models.Chat
{
    public class ChatUserConversation
    {
    public string ChatUserId { get; set; } // Foreign Key
    public ChatUser? ChatUser { get; set; } // Navigation property

    public string ConversationId { get; set; } // Foreign Key
    public Conversation? Conversation { get; set; } // Navigation property
    }
}