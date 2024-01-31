using ChattingApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ChattingApp.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Conversation>()
               .HasOne(c => c.Room)
               .WithOne(r => r.Conversation)
               .HasForeignKey<Room>(r => r.ConversationId)
               .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Conversation> Conversations { get; set; }
        //DB Sets will be here
    }
}
