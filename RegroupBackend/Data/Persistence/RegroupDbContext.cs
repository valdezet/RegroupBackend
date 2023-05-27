using Microsoft.EntityFrameworkCore;

namespace RegroupBackend.Data.Persistence;

public class RegroupDbContext : DbContext
{
    public DbSet<ChatRoom> ChatRooms { get; set; }

    public DbSet<ChatRoomMessages> ChatRoomMessages { get; set; }

    public DbSet<ChatRoomUser> ChatRoomUsers { get; set; }

    public DbSet<ChatRoomInvites> ChatRoomInvites { get; set; }

    public RegroupDbContext(DbContextOptions<RegroupDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ChatRoomMessages>()
            .HasOne(message => message.Room)
            .WithMany(room=> room.Messages)
            .OnDelete(DeleteBehavior.NoAction);
            
    }
}
