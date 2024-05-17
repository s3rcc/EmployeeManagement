using AutoMapper;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using EmployeeManagement.RepositoryInterfaces;

namespace EmployeeManagement.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly IMapper _mapper;
        private readonly ISalaryRepository _salaryRepository;
        private readonly IUserRepository _userRepository;
        public SalaryService(IMapper mapper, ISalaryRepository salaryRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _salaryRepository = salaryRepository;
            _userRepository = userRepository;
        }

        public void AddSalary(SalaryDTO salaryDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteSalary(int salaryId)
        {
            throw new NotImplementedException();
        }

        public ICollection<SalaryDTO> GetAllSalarys()
        {
            throw new NotImplementedException();
        }

        public SalaryDTO GetSalaryById(int salaryId)
        {
            throw new NotImplementedException();
        }

        public void UpdateSalary(SalaryDTO salaryDto)
        {
            throw new NotImplementedException();
        }
    }
}
