using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    [Table("Form")]
    public class Form
    {
        [Key]
        public int FormID { get; set; }
        [ForeignKey("UserID")]
        public int UserID { get; set; }
        public User User { get; set; }
        [ForeignKey("TypeID")]
        public int TypeID { get; set; }
        public FormType FormType;
        public string FormName { get; set; }
        [Column(TypeName = "text")]
        public string FormDescription { get; set; }
        [Column(TypeName = "varbinary(MAX)")]
        public byte[] Attachments { get; set; }
        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
