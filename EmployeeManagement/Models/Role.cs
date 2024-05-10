using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    [Table("Role")]
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        [Column(TypeName ="nvarchar(50)")]
        public string RoleName { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Claim> Claims { get; set; }
    }
}
