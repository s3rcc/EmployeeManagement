using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    public interface IFormRepository
    {
        ICollection<Form> GetForms();
        Form GetForm(int id);
        ICollection<Form> GetFormByUserId(int id);
        ICollection<Form> GetFormByTypeId(int id);
        ICollection<Form> GetFormName(string name);
        bool FormExists(int formId);
        bool CreateForm(Form form);
        bool UpdateForm(Form form);
        bool DeleteForm(Form form);
    }
}
