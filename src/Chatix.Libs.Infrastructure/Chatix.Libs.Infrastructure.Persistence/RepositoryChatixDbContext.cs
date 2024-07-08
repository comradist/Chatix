using Chatix.Libs.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace Chatix.Libs.Infrastructure.Persistence;

public class RepositoryChatixDbContext : DbContext
{
    public RepositoryChatixDbContext(DbContextOptions<RepositoryChatixDbContext> options) : base(options)
    {
    }

    public DbSet<Message> Messages { get; set; }

    public DbSet<Room> Rooms { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<RoomUser> RoomUsers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<RoomUser>()
            .HasKey(ru => new { ru.RoomId, ru.UserId });

        modelBuilder.Entity<RoomUser>()
            .HasOne(ru => ru.Room)
            .WithMany(r => r.RoomUsers)
            .HasForeignKey(ru => ru.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RoomUser>()
            .HasOne(ru => ru.User)
            .WithMany(u => u.RoomUsers)
            .HasForeignKey(ru => ru.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.ToRoom)
            .WithMany(r => r.Messages)
            .HasForeignKey(m => m.ToRoomId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Room>()
            .HasOne(r => r.Admin)
            .WithMany(u => u.CreatedRooms)
            .HasForeignKey(r => r.AdminId)
            .OnDelete(DeleteBehavior.Cascade);




        // modelBuilder.Entity<User>()
        //     .Navigation(u => u.CreatedRooms).AutoInclude();
        // modelBuilder.Entity<User>()
        //     .Navigation(u => u.Messages).AutoInclude();
        modelBuilder.Entity<User>()
            .Navigation(u => u.RoomUsers).AutoInclude();

        // modelBuilder.Entity<Room>()
        //     .Navigation(r => r.Messages).AutoInclude();
        modelBuilder.Entity<Room>()
            .Navigation(r => r.RoomUsers).AutoInclude();
        // modelBuilder.Entity<Room>()
        //     .Navigation(r => r.Admin).AutoInclude();




        base.OnModelCreating(modelBuilder);

    }

    // public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    // {
    // foreach(var entry in ChangeTracker.Entries<BaseDomainEntity>())
    // {
    //     entry.Entity.LastModifiedBy = "Comrade";
    //     entry.Entity.LastModifiedData = DateTime.UtcNow;
    //     if(entry.State == EntityState.Added)
    //     {
    //         entry.Entity.CreatedBy = "Comrade";
    //         entry.Entity.DataCreated = DateTime.UtcNow;
    //     }
    // }

    //     return base.SaveChangesAsync(cancellationToken);
    // }
}
