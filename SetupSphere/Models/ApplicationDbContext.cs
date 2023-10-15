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
            .HasForeignKey(m => m.ReceiverID); // Message refers to User via ReceiverID
        
        // Configures the one-to-many relationship for the Receiver
        modelBuilder.Entity<Message>()
            .HasOne<User>(u => u.Receiver) // Message has one User as Receiver 
            .WithMany(u => u.MessagesRecieved) // User includes multiple MessagesReceived
            .HasForeignKey(m => m.SenderID); // Message refers to to User via SenderID
    }
}