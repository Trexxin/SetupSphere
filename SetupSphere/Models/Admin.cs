using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SetupSphere.Models;

public class Admin
{
    // Primary Key
    [Key] public int AdminID { get; set; }
    
    // Foreign key for user
    public int UserID { get; set; }
    [ForeignKey("UserID")] public User User { get; set; }
    
    public string Privileges { get; set; }
}