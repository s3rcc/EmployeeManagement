using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    public interface IRoleRepository
    {
        ICollection<Role> GetRoles();
        Role GetRole(int id);
        Role GetRoleName(string roleName);
        bool RoleExists(int roleId);
    }
}
