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
    public class ChatUser
    {
        [Key]
        [Required]
        public required string Id {get;set;}
        public  string? AliasName {get;set;}

        
        public  string? Avatar {get;set;}
        [Required]
        

        public required string ApplicationUserId {get; set;}
        [ForeignKey("ApplicationUserId")]

        // Navigation property
        [ValidateNever]
        public ApplicationUser? ApplicationUser {get; set;}

        public ICollection<ChatUserConversation> ChatUserConversation { get; set; }
    }
}