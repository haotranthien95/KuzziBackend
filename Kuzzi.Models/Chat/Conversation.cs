using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Kuzzi.Models.Auth;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Kuzzi.Models.Chat
{
    public class Conversation
    {
        [Key]
        [Required]
        public required string Id {get;set;}
        [Required]

        public DateTime CreatedAt {get; set;}
        [Required]

        public DateTime LastUpdated {get; set;}
        
        public string? ConversationType {get; set;}

        public string? CreatedUserId {get; set;}
        [ForeignKey("CreatedUserId")]
        [ValidateNever]
        public ApplicationUser? CreatedUser {get; set;}
    }
}