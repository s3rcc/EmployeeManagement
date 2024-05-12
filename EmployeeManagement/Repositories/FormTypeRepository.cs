using EmployeeManagement.Data;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.Repositories
{
    public class FormTypeRepository : IFormTypeRepository
    {
        private readonly DataContext _context;
        public FormTypeRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateFormType(FormType formType)
        {
            _context.FormTypes.Add(formType);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteFormType(FormType formType)
        {
            _context.FormTypes.Remove(formType);
            _context.SaveChanges();
            return true;
        }

        public bool FormTypeExists(int formTypeId)
        {
            return _context.FormTypes.Any(x => x.TypeID == formTypeId);
        }

        public FormType GetFormType(int id)
        {
            return _context.FormTypes.Where(x => x.TypeID == id).FirstOrDefault();
        }

        public ICollection<FormType> GetFormTypeName(string name)
        {
            return _context.FormTypes.Where(x => x.Name.Contains(name)).ToList();
        }

        //public FormType GetFormTypeName(string name)
        //{
        //    return _context.FormTypes.Where(x => x.Name == name).FirstOrDefault();
        //}

        public ICollection<FormType> GetFormTypes()
        {
            return _context.FormTypes.OrderBy(x => x.TypeID).ToList();
        }

        public bool UpdateFormType(FormType formType)
        {
            _context.FormTypes.Update(formType);
            _context.SaveChanges();
            return true;
        }
    }
}
