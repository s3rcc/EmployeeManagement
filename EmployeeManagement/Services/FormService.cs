using AutoMapper;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using EmployeeManagement.RepositoryInterfaces;
using System.Security.Claims;

namespace EmployeeManagement.Services
{
    public class FormService : IFormService
    {
        private readonly IMapper _mapper;
        private readonly IFormRepository _formRepository;
        private readonly IFormTypeRepository _formTypeRepository;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly ILogger<FormService> _logger;
        public FormService(IMapper mapper, IFormRepository formRepository, IFormTypeRepository formTypeRepository, IHttpContextAccessor httpContextAccessor, ILogger<FormService> logger)
        {
            _mapper = mapper;
            _formRepository = formRepository;
            _formTypeRepository = formTypeRepository;
            _HttpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public void AddAttachments(int formId, List<byte[]> attachments)
        {
            if (!_formRepository.FormExists(formId))
                throw new InvalidOperationException($"Form with ID {formId} does not exist.");
            var form = _formRepository.GetForm(formId);
            if (form == null)
                throw new InvalidOperationException($"Form with ID {formId} does not exist.");

            if (attachments != null && attachments.Count > 0)
            {
                form.Attachments = attachments.SelectMany(a => a).ToArray(); // Concatenate byte arrays
            }

            if (!_formRepository.UpdateForm(form))
                throw new Exception("Adding attachments failed.");
        }

        public List<byte[]> GetAttachments(int formId)
        {
            // Check if form exists
            if (!_formRepository.FormExists(formId))
                throw new InvalidOperationException($"Form with ID {formId} does not exist.");

            // Retrieve the form with attachments
            var form = _formRepository.GetForm(formId);

            // Check if attachments exist
            if (form.Attachments == null || form.Attachments.Length == 0)
                return new List<byte[]>(); // No attachments found, return an empty list

            // Convert attachments to a list and return
            return new List<byte[]> { form.Attachments };
        }

        public void AddForm(AddFormDTO formDto, List<byte[]> attachments)
        {
            var userId = GetUserIdFromToken();

            // Logging: Log the incoming form DTO
            _logger.LogInformation("Received form DTO: {@FormDto}", formDto);

            if (!_formTypeRepository.FormTypeExists(formDto.TypeID))
            {
                // Logging: Log the missing form type ID
                _logger.LogError("FormType with ID {FormTypeId} does not exist.", formDto.TypeID);
                throw new InvalidOperationException($"FormType with ID {formDto.TypeID} does not exist.");
            }

            if (formDto == null)
            {
                // Logging: Log the invalid form data
                _logger.LogError("Form data is invalid: {@FormData}", formDto);
                throw new ArgumentException("Form data is invalid.");
            }

            var form = _mapper.Map<Form>(formDto);
            _logger.LogInformation("Mapped form DTO to form entity: {@FormEntity}", form);

            form.UserID = userId;

            if (attachments != null && attachments.Count > 0)
            {
                // Logging: Log the attachments count
                _logger.LogInformation("Received {AttachmentsCount} attachments.", attachments.Count);

                // Logging: Log the concatenation of byte arrays
                form.Attachments = attachments.SelectMany(a => a).ToArray(); // Concatenate byte arrays
                _logger.LogInformation("Concatenated attachments: {Attachments}", form.Attachments);
            }

            // Logging: Log the creation of the form
            _logger.LogInformation("Creating form: {@Form}", form);
            _formRepository.CreateForm(form);
        }

        public void DeleteForm(int formId)
        {
            var form = _formRepository.GetForm(formId);
            if (form == null)
                throw new InvalidOperationException($"Form with ID {formId} does not exist.");

            if (!_formRepository.DeleteForm(form))
                throw new Exception("Deleting form failed.");
        }

        public FormDTO GetFormById(int id)
        {
            var form = _formRepository.GetForm(id);
            if (form == null)
                throw new InvalidOperationException($"Form with ID {id} does not exist.");

            var formDto = _mapper.Map<FormDTO>(form);
            return formDto;
        }

        public ICollection<FormDTO> GetFormByName(string name)
        {
            var forms = _formRepository.GetFormName(name);
            if (forms == null)
            {
                throw new InvalidOperationException($"No result found!");
            }
            return _mapper.Map<ICollection<FormDTO>>(forms);
        }

        public ICollection<FormDTO> GetFormByTypeId(int id)
        {
            var forms = _formRepository.GetFormByTypeId(id);
            if (forms == null)
            {
                throw new InvalidOperationException($"No result found!");
            }
            return _mapper.Map<ICollection<FormDTO>>(forms);
        }

        public ICollection<FormDTO> GetFormByUserId(int id)
        {
            var forms = _formRepository.GetFormByUserId(id);
            if (forms == null)
            {
                throw new InvalidOperationException($"No result found!");
            }
            return _mapper.Map<ICollection<FormDTO>>(forms);
        }

        public ICollection<FormDTO> GetForms()
        {
            var forms = _formRepository.GetForms();
            if (forms == null)
            {
                throw new InvalidOperationException($"No result found!");
            }
            return _mapper.Map<ICollection<FormDTO>>(forms);
        }

        public void UpdateForm(int formId, FormDTO formDto, List<byte[]> attachments)
        {
            if (!_formTypeRepository.FormTypeExists(formDto.TypeID))
                throw new InvalidOperationException($"FormType with ID {formDto.TypeID} does not exist.");

            if (formDto == null || formId != formDto.FormID)
                throw new ArgumentException("Form data is invalid.");

            var formExists = _formRepository.FormExists(formId);
            if (!formExists)
                throw new InvalidOperationException($"Form with ID {formId} does not exist.");

            var form = _mapper.Map<Form>(formDto);
            if (attachments != null && attachments.Count > 0)
            {
                form.Attachments = attachments[0]; // Handle only the first file for simplicity
            }

            if (!_formRepository.UpdateForm(form))
                throw new Exception("Updating form failed.");
        }
        public int GetUserIdFromToken()
        {
            return int.Parse(_HttpContextAccessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}