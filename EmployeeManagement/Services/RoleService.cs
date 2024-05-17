using AutoMapper;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using EmployeeManagement.RepositoryInterfaces;

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
            //Check valid data
            if (roleDto == null || string.IsNullOrWhiteSpace(roleDto.RoleName))
            {
                throw new ArgumentException("Role data is invalid.");
            }
            //Check if the role already exists
            if (_roleRepository.RoleExists(roleDto.RoleID))
                {
                    throw new InvalidOperationException($"Role with ID {roleDto.RoleID} already exists!");
                }
                //Create a new role
                var newRole = _mapper.Map<Role>(roleDto);
                _roleRepository.CreateRole(newRole);
        }

        public void DeleteRole(int roleId)
        {
                var role = _roleRepository.GetRole(roleId);
            //Check if the role exists
            if (role == null)
            {
                throw new InvalidOperationException($"Role with ID {roleId} does not exist!");
            }
            //Process to delete the role
            _roleRepository.DeleteRole(role);
        }

        public ICollection<RoleDTO> GetAllRoles()
        {
            var roles = _roleRepository.GetRoles();
            if (roles == null)
            {
                throw new InvalidOperationException($"No result found!");
            }
            return _mapper.Map<ICollection<RoleDTO>>(roles);
        }

        public RoleDTO GetRoleById(int roleId)
        {
            //Check if the role exists
            if (!_roleRepository.RoleExists(roleId))
            {
                throw new InvalidOperationException($"Role with ID {roleId} does not exist!");
            }
            //Get the roleDto
            var role = _roleRepository.GetRole(roleId);
            return _mapper.Map<RoleDTO>(role);
        }

        public void UpdateRole(RoleDTO roleDto)
        {
            //Check valid data
            if (roleDto == null || string.IsNullOrWhiteSpace(roleDto.RoleName))
            {
                throw new ArgumentException("Role data is invalid.");
            }
            //Check if the role exists
            if (!_roleRepository.RoleExists(roleDto.RoleID))
                {
                    throw new InvalidOperationException($"Role with ID {roleDto.RoleID} does not exist!");
                }
                //Update the role
                var updatedRole = _mapper.Map<Role>(roleDto);
                _roleRepository.UpdateRole(updatedRole);
        }
    }
}
