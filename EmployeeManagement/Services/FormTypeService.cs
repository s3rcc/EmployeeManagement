using AutoMapper;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using System.Security.Claims;

namespace EmployeeManagement.Services
{
    public class FormTypeService : IFormTypeService
    {
        private readonly IMapper _mapper;
        private readonly IFormTypeRepository _formTypeRepository;
        public FormTypeService(IFormTypeRepository formTypeRepository, IMapper mapper)
        {
            _mapper = mapper;
            _formTypeRepository = formTypeRepository;
        }
        public void AddFormType(FormTypeDTO formTypeDto)
        {
            //Check valid data
            if (formTypeDto == null || string.IsNullOrWhiteSpace(formTypeDto.Name))
            {
                throw new ArgumentException("Form type data is invalid.");
            }
            //Check Form type exists
            if (_formTypeRepository.FormTypeExists(formTypeDto.TypeID))
            {
                throw new InvalidOperationException($"Form type with ID {formTypeDto.TypeID} already exists!");
            }
            //Create a new form type
            var newFormType = _mapper.Map<FormType>(formTypeDto);
            _formTypeRepository.CreateFormType(newFormType);
        }

        public void DeleteFormType(int typeId)
        {
            var formType = _formTypeRepository.GetFormType(typeId);
            //Check if the form type exists
            if (formType == null)
            {
                throw new InvalidOperationException($"Form type with ID {typeId} does not exist!");
            }
            //Process to delete the role
            _formTypeRepository.DeleteFormType(formType);
        }

        public FormTypeDTO GetFormById(int id)
        {
            var formType = _formTypeRepository.GetFormType(id);
            if (formType == null)
            {
                throw new InvalidOperationException($"No result found!");
            }
            return _mapper.Map<FormTypeDTO>(formType);
        }

        public ICollection<FormTypeDTO> GetFormByName(string name)
        {
            var formTypes = _formTypeRepository.GetFormTypeName(name);
            if (formTypes == null)
            {
                throw new InvalidOperationException($"No result found!");
            }
            return _mapper.Map<ICollection<FormTypeDTO>>(formTypes);
        }

        //public FormTypeDTO GetFormByName(string name)
        //{
        //    var formType = _formTypeRepository.GetFormTypeName(name);
        //    return _mapper.Map<FormTypeDTO>(formType);
        //}

        public ICollection<FormTypeDTO> GetFormTypes()
        {
            var formTypes = _formTypeRepository.GetFormTypes();
            if (formTypes == null)
            {
                throw new InvalidOperationException($"No result found!");
            }
            return _mapper.Map<ICollection<FormTypeDTO>>(formTypes);
        }

        public void UpdateFormType(FormTypeDTO formTypeDto)
        {
            //Check valid data
            if (formTypeDto == null || string.IsNullOrWhiteSpace(formTypeDto.Name))
            {
                throw new ArgumentException("Form type data is invalid.");
            }
            //Check if the form type exists
            if (!_formTypeRepository.FormTypeExists(formTypeDto.TypeID))
            {
                throw new InvalidOperationException($"Form type with ID {formTypeDto.TypeID} does not exist!");
            }
            //Update the form type
            var updatedFormType = _mapper.Map<FormType>(formTypeDto);
            _formTypeRepository.UpdateFormType(updatedFormType);
        }
    }
}
