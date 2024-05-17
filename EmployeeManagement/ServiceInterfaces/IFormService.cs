using EmployeeManagement.Dto;

namespace EmployeeManagement.Interfaces
{
    public interface IFormService
    {
        ICollection<FormDTO> GetForms();
        FormDTO GetFormById(int id);
        ICollection<FormDTO> GetFormByName(string name);
        ICollection<FormDTO> GetFormByUserId(int id);
        ICollection<FormDTO> GetFormByTypeId(int id);
        public void AddForm(AddFormDTO formDto, List<byte[]> attachments);
        void UpdateForm(int formId, FormDTO formDto, List<byte[]> attachments);
        void DeleteForm(int formId);
        List<byte[]> GetAttachments(int formId);
        void AddAttachments(int formId, List<byte[]> attachments);
    }
}