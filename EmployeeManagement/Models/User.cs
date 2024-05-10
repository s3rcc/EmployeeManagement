using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Phone { get; set; }
        [ForeignKey("RoleID")]
        public int RoleID { get; set; }
        public Role Role { get; set; }
        [ForeignKey("SalaryID")]
        public int SalaryID { get; set; }
        public Salary Salary { get; set; }
        public ICollection<Form> Forms { get; set; }
        public ICollection<UserClaim> UserClaims { get; set; }
    }
}
