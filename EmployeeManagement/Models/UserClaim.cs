using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    [Table("User_Claim")]
    public class UserClaim
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("ClaimID")]
        public int ClaimID { get; set; }
        public Claim Claim { get; set; }
        [ForeignKey("UserID")]
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
