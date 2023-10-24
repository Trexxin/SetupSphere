using System.ComponentModel.DataAnnotations;

namespace SetupSphere.Models
{
    public class User
    {
        // Primary Key
        [Key] public int UserID { get; set; }
        
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime DateJoined { get; set; }
        
        // Navigation properties
        // These are virtual for Entity Framework's lazy-loading feature
        
        // Setups related to the user
        public virtual ICollection<Setup> Setups { get; set; }
        
        // Comments made by the user
        public virtual ICollection<Comment> Comments { get; set; }
        
        // Messages sent by the user
        public virtual ICollection<Message> MessagesSent { get; set; }
        
        // Messages received by the user
        public virtual ICollection<Message> MessagesRecieved { get; set; }
    }
}

