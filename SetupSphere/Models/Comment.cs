using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SetupSphere.Models
{
    public class Comment
    {
        // Primary Key
        [Key] public int CommentID { get; set; }
        
        // Foreign Key for Setup
        public int SetupID { get; set; }
        [ForeignKey("SetupID")] public Setup Setup { get; set; }
        
        // Foreign Key for User
        public int UserID { get; set; }
        [ForeignKey("UserID")] public User User { get; set; }
        
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}

