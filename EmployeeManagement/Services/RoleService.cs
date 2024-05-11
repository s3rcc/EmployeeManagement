using AutoMapper;
using EmployeeManagement.Data;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;

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
                if (_roleRepository.RoleExists(roleDto.RoleID))
                {
                    throw new InvalidOperationException($"Role with ID {roleDto.RoleID} already exists!");
                }
                var newRole = _mapper.Map<Role>(roleDto);
                _roleRepository.CreateRole(newRole);
        }

        public void DeleteRole(int roleId)
        {
                var role = _roleRepository.GetRole(roleId);
            if (role == null)
            {
                throw new InvalidOperationException($"Role with ID {roleId} does not exist!");
            }
            _roleRepository.DeleteRole(role);
        }

        public ICollection<RoleDTO> GetAllRoles()
        {
            var roles = _roleRepository.GetRoles();
            return _mapper.Map<ICollection<RoleDTO>>(roles);
        }

        public RoleDTO GetRoleById(int roleId)
        {
            if (!_roleRepository.RoleExists(roleId))
            {
                throw new InvalidOperationException($"Role with ID {roleId} does not exist!");
            }

            var role = _roleRepository.GetRole(roleId);
            return _mapper.Map<RoleDTO>(role);
        }

        public void UpdateRole(RoleDTO roleDto)
        {
                if (!_roleRepository.RoleExists(roleDto.RoleID))
                {
                    throw new InvalidOperationException($"Role with ID {roleDto.RoleID} does not exist!");
                }
                var updatedRole = _mapper.Map<Role>(roleDto);
                _roleRepository.UpdateRole(updatedRole);
        }
    }
}
