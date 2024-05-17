using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly DataContext _context;
        public FormRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateForm(Form form)
        {
            _context.Forms.Add(form);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteForm(Form form)
        {
            _context.Forms.Remove(form);
            _context.SaveChanges();
            return true;
        }

        public bool FormExists(int formId)
        {
            return _context.Forms.Any(x => x.FormID == formId);
        }

        public ICollection<byte[]> GetAttachmentsForForm(int formId)
        {
            var form = _context.Forms.FirstOrDefault(f => f.FormID == formId);
            if (form != null)
            {
                return new List<byte[]> { form.Attachments };
            }
            return new List<byte[]>(); 
        }

        public Form GetForm(int id)
        {
            return _context.Forms.Where(x => x.FormID == id).FirstOrDefault();
        }

        public ICollection<Form> GetFormByTypeId(int id)
        {
            return _context.Forms.Where(x => x.TypeID == id).ToList();
        }

        public ICollection<Form> GetFormByUserId(int id)
        {
            return _context.Forms.Where(x => x.UserID == id).ToList();
        }

        public ICollection<Form> GetFormName(string name)
        {
            return _context.Forms.Where(x => x.FormName.Contains(name)).ToList();
        }

        public ICollection<Form> GetForms()
        {
            return _context.Forms.OrderBy(x => x.FormID).ToList();
        }

        public bool UpdateForm(Form form)
        {
            _context.Forms.Update(form);
            _context.SaveChanges();
            return true;
        }
    }
}
