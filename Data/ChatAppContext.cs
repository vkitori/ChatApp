using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChatApp.Models;

namespace ChatApp.Data
{
    public class ChatAppContext : DbContext
    {
        public ChatAppContext (DbContextOptions<ChatAppContext> options)
            : base(options)
        {
        }

        public DbSet<ChatApp.Models.User> User { get; set; } = default!;
        public DbSet<ChatApp.Models.Message> Message { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasOne(m => m.User) 
                .WithMany(u => u.Messages) 
                .HasForeignKey(m => m.UserId);

            modelBuilder.Entity<Message>()
               .HasOne(m => m.ChatRoom)
               .WithMany(cr => cr.Messages)
               .HasForeignKey(m => m.ChatRoomId);
        }
        public DbSet<ChatApp.Models.ChatRoom> ChatRoom { get; set; } = default!;
    }
}
