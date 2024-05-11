using EmployeeManagement.Dto;

namespace EmployeeManagement.Interfaces
{
    public interface IRoleService
    {
        ICollection<RoleDTO> GetAllRoles();
        RoleDTO GetRoleById(int roleId);
        void AddRole(RoleDTO roleDto);
        void UpdateRole(RoleDTO roleDto);
        void DeleteRole(int roleId);
    }
}
