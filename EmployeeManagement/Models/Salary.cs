using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    [Table("Salary")]
    public class Salary
    {
        [Key]
        public int SalaryID { get; set; }
        [Column(TypeName = "money")]
        public decimal BaseSalary { get; set; }
        [Column(TypeName = "money")]
        public decimal DailyRate { get; set; }
        [ForeignKey("UserID")]
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
