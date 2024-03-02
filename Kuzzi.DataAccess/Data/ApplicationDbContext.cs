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

        protected override void OnModelCreating(ModelBuilder modelBuilder){
           
          base.OnModelCreating(modelBuilder);
          modelBuilder.Entity<Conversation>().HasData(
                new Conversation { Id = 1, Name = "Action" },
                new Conversation { Id = 2, Name = "Active",  },
                new Conversation { Id = 3, Name = "Movie",  }
                );
        }}
    
}