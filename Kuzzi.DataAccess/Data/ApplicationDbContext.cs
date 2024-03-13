using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuzzi.Models.Auth;
using Kuzzi.Models.Chat;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kuzzi.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }    

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Conversation> Conversation { get; set; }
        public DbSet<ChatUser> ChatUser { get; set; }
        public DbSet<ChatUserConversation> ChatUserConversation { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<SeenMessage> SeenMessage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
           
          base.OnModelCreating(modelBuilder);

          modelBuilder.Entity<ChatUserConversation>().HasKey(sc => new {sc.ChatUserId, sc.ConversationId});
          modelBuilder.Entity<ChatUserConversation>().HasOne(sc => sc.ChatUser).WithMany(s => s.ChatUserConversation).HasForeignKey(sc => sc.ChatUserId);
          modelBuilder.Entity<ChatUserConversation>().HasOne(sc => sc.Conversation).WithMany(s => s.ChatUserConversation).HasForeignKey(sc => sc.ConversationId);


          // modelBuilder.Entity<Conversation>().HasData(
          //       new Conversation { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.UtcNow,LastUpdated = DateTime.UtcNow }

          //       );
        }}
    
}