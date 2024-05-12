using EmployeeManagement.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace EmployeeManagement.Dto
{
    public class ClaimDTO
    {
        public int ClaimID { get; set; }
        public string ClaimType { get; set; }
        public bool ClaimValue { get; set; }
        public int RoleID { get; set; }
    }
}
