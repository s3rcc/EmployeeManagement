﻿using EmployeeManagement.Models;
namespace EmployeeManagement.Interfaces
{
    public interface ISalaryRepository
    {
        ICollection<Salary> GetSalarys();
        Salary GetSalary(int id);
        bool SalaryExists(int salaryId);
        bool CreateSalary(Salary salary);
        bool UpdateSalary(Salary salary);
        bool DeleteSalary(Salary salary);
    }
}
