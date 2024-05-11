using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    [Table("Claim")]
    public class Claim
    {
        [Key]
        public int ClaimID { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string ClaimType { get; set; }
        [Column(TypeName = "bit")]
        public bool ClaimValue { get; set; }
        [ForeignKey("RoleID")]
        public int RoleID { get; set; }
        public Role Role { get; set; }
        public ICollection<UserClaim> UserClaims { get; set; }
    }
}
