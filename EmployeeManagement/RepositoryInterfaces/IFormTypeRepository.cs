using EmployeeManagement.Models;

namespace EmployeeManagement.RepositoryInterfaces
{
    public interface IFormTypeRepository
    {
        ICollection<FormType> GetFormTypes();
        FormType GetFormType(int id);
        ICollection<FormType> GetFormTypeName(string name);
        bool FormTypeExists(int formTypeId);
        bool CreateFormType(FormType formType);
        bool UpdateFormType(FormType formType);
        bool DeleteFormType(FormType formType);
    }
}
