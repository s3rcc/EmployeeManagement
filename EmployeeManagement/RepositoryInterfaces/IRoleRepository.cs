using EmployeeManagement.Models;

namespace EmployeeManagement.RepositoryInterfaces
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
    }
}
