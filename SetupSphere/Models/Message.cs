using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SetupSphere.Models
{
    public class Message
    {
        // Primary Key
        [Key] public int MessageID { get; set; }
        
        // Foreign key for User
        public int SenderID { get; set; }
        [ForeignKey("SenderID")] public User Sender { get; set; }

        // Foreign key for Receiver
        public int ReceiverID { get; set; }
        [ForeignKey("ReceiverID")] public User Receiver { get; set; }
        
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}
