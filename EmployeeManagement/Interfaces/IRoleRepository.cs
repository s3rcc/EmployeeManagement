using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    public interface IRoleRepository
    {
        ICollection<Role> GetRoles();
        Role GetRole(int id);
        Role GetRoleName(string roleName);
        bool RoleExists(int roleId);
        bool CreateRole(Role role);
        bool UpdateRole(Role role);
        bool DeleteRole(Role role);
        bool Save();
    }
}
