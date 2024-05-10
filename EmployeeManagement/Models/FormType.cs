using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    [Table("Form_Type")]
    public class FormType
    {
        [Key]
        public int TypeID { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }
        public ICollection<Form> Forms { get; set; }
    }
}
