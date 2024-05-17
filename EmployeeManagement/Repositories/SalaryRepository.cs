using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.RepositoryInterfaces;

namespace EmployeeManagement.Repositories
{
    public class SalaryRepository : ISalaryRepository
    {
        private readonly DataContext _context;
        public SalaryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateSalary(Salary salary)
        {
            _context.Salaries.Add(salary);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteSalary(Salary salary)
        {
            _context.Salaries.Remove(salary);
            _context.SaveChanges();
            return true;
        }

        public Salary GetSalary(int id)
        {
            return _context.Salaries.Where(x => x.SalaryID == id).FirstOrDefault();
        }

        public ICollection<Salary> GetSalarys()
        {
            return _context.Salaries.OrderBy(x => x.SalaryID).ToList();
        }

        public bool SalaryExists(int salaryId)
        {
            return _context.Salaries.Any(x => x.SalaryID == salaryId);
        }

        public bool UpdateSalary(Salary salary)
        {
            _context.Salaries.Update(salary);
            _context.SaveChanges();
            return true;
        }
    }
}
