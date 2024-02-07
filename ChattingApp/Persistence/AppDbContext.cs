using ChattingApp.Models;
using Microsoft.EntityFrameworkCore;

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
               .HasForeignKey<Conversation>(c=> c.RoomId)
               .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Message>()
                .Property(m => m.Type)
                .HasConversion(
                     m => m.ToString(), // this line i store in db
                     m => (MessageType)Enum.Parse(typeof(MessageType), m)  // this line i Retrieve avaraible from db
                );
            modelBuilder.Entity<Message>()
                .Property(m => m.Direction)
                .HasConversion(
                     m => m.ToString(), // this line i store in db
                     m => (Direction)Enum.Parse(typeof(Direction), m)  // this line i Retrieve avaraible from db
                );
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Conversation> Conversations { get; set; }
        //DB Sets will be here
    }
}
