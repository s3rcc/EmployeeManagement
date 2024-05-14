using EmployeeManagement.Dto;

namespace EmployeeManagement.Interfaces
{
    public interface ISalaryService
    {
        ICollection<SalaryDTO> GetAllSalarys();
        SalaryDTO GetSalaryById(int salaryId);
        void AddSalary(SalaryDTO salaryDto);
        void UpdateSalary(SalaryDTO salaryDto);
        void DeleteSalary(int salaryId);
    }
}
