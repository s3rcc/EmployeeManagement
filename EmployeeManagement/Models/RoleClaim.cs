using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    [Table("Role_Claim")]
    public class RoleClaim
    {
        public int RoleID { get; set; }
        public Role Role { get; set; }
        public int ClaimID { get; set; }
        public Claim Claim { get; set; }
    }
}
