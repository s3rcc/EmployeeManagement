using AutoMapper;
using EmployeeManagement.Data;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;

namespace EmployeeManagement.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public void AddRole(RoleDTO roleDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteRole(int roleId)
        {
            throw new NotImplementedException();
        }

        public ICollection<RoleDTO> GetAllRoles()
        {
            var roles = _roleRepository.GetRoles();
            return _mapper.Map<ICollection<RoleDTO>>(roles);

        }

        public RoleDTO GetRoleById(int roleId)
        {
            throw new NotImplementedException();
        }

        public void UpdateRole(RoleDTO roleDto)
        {
            throw new NotImplementedException();
        }
    }
}
