using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kuzzi.Models.Chat
{
    public class SeenMessage
    {
        [Required]
        public required string Id {get; set;}
        [Required]
        public required string MessageId {get; set;}
        [ForeignKey("MessageId")]
        public  Message? Message {get; set;}
        public required DateTime SeenAt { get; set; }

    }
}