using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    [Table("User_Claim")]
    public class UserClaim
    {
        public int ClaimID { get; set; }
        public Claim Claim { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        [DefaultValue(true)]
        public bool? IsClaim { get; set; }
    }
}
