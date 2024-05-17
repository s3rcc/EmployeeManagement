using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    [Table("RefreshToken")]
    public class RefreshToken
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("UserID")]
        public int UserID { get; set; }
        public User User { get; set; }
        public string? Token { get; set; }
        public DateTime Expire { get; set;}
        public bool IsActive { get; set;}
    }
}
