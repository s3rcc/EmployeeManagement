using EmployeeManagement.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Dto
{
    public class FormDTO
    {
        public int FormID { get; set; }
        public int UserID { get; set; }
        public int TypeID { get; set; }
        public string FormName { get; set; }
        public string FormDescription { get; set; }
    }
}
