using EmployeeManagement.Dto;
using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    public interface IFormTypeService
    {
        ICollection<FormTypeDTO> GetFormTypes();
        FormTypeDTO GetFormById(int id);
        ICollection<FormTypeDTO> GetFormByName(string name);
        void AddFormType(FormTypeDTO formTypeDto);
        void UpdateFormType(FormTypeDTO formTypeDto);
        void DeleteFormType(int typeId);

    }
}
