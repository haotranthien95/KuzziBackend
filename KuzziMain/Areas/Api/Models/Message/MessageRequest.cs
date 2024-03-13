using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KuzziMain.Areas.Api.Models.Message
{
    public class MessageRequest
    {
        public required string LocalMessageId {get; set;}
        public required string ConversationId {get; set;}
        public string? Value {get; set;}
        public string? Text {get; set;}
        public string? Type {get; set;}
        public string? ReplyTo {get; set;}

    }
}