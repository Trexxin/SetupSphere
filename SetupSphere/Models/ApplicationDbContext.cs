using Microsoft.EntityFrameworkCore;
namespace SetupSphere.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    // Defines a DBSet for each entity
    public DbSet<User> Users { get; set; }
    public DbSet<Setup> Setups { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Admin> Admins { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configures the one-to-many relationship for the Sender in Messages
        modelBuilder.Entity<Message>()
            .HasOne<User>(m => m.Sender) // Message has one User as Sender
            .WithMany(u => u.MessagesSent) // User includes multiple MessagesSent
            .HasForeignKey(m => m.SenderID) // Message refers to to User via SenderID
            .OnDelete(DeleteBehavior.Restrict); // Ensures that the delete operation doesn't cascade 
        
        // Configures the one-to-many relationship for the Receiver
        modelBuilder.Entity<Message>()
            .HasOne<User>(u => u.Receiver) // Message has one User as Receiver 
            .WithMany(u => u.MessagesRecieved) // User includes multiple MessagesReceived
            .HasForeignKey(m => m.ReceiverID) // Message refers to User via ReceiverID
            .OnDelete(DeleteBehavior.Restrict); // Ensures that the delete operation doesn't cascade 

        // Configuring the relationship between User and Setups
        modelBuilder.Entity<Setup>()
            .HasOne(s => s.User)  // Each Setup has one User
            .WithMany(u => u.Setups)  // Each User has many Setups
            .HasForeignKey(s => s.UserID)  // Setup has a foreign key UserID
            .OnDelete(DeleteBehavior.Restrict);  // Restricting the delete behavior

        // Configuring the relationship between Setup and Comments
        modelBuilder.Entity<Setup>()
            .HasMany(s => s.Comments)  // Each Setup has many Comments
            .WithOne(c => c.Setup)  // Each Comment belongs to one Setup
            .HasForeignKey(c => c.SetupID)  // Comment has a foreign key SetupID
            .OnDelete(DeleteBehavior.Restrict);  // Restricting the delete behavior

        // Configuring the relationship between User and Comments
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)  // Each Comment has one User
            .WithMany(u => u.Comments)  // Each User has many Comments
            .HasForeignKey(c => c.UserID)  // Comment has a foreign key UserID
            .OnDelete(DeleteBehavior.Restrict);  // Restricting the delete behavior
    }
}