using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SetupSphere.Models
{
    public class Setup
    {
        // Primary Key
        [Key] public int SetupID { get; set; }
        
        // Foreign key for User
        public int UserID { get; set; }
        [ForeignKey("UserID")] public User User { get; set; }

        public string PhotoURL { get; set; }
        public string Description { get; set; }
        public string HardwareDetails { get; set; }
        public DateTime DatePosted { get; set; }
        
        // Navigation property
        // It is virtual for Entity Framework's lazy-loading feature
        
        // Comments on the setup
        public virtual ICollection<Comment> Comments { get; set; }
    }
}


