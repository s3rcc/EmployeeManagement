using EmployeeManagement.Data;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private DataContext _context;
        public RoleRepository()
        {

        }

        public bool CreateRole(Role role)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRole(Role role)
        {
            throw new NotImplementedException();
        }

        public Role GetRole(int id)
        {
            return _context.Roles.Where(r => r.RoleID == id).FirstOrDefault();
        }

        public Role GetRoleName(string roleName)
        {
            return _context.Roles.Where(r => r.RoleName == roleName).FirstOrDefault();
        }

        public ICollection<Role> GetRoles()
        {
            return _context.Roles.OrderBy(r => r.RoleID).ToList();
        }

        public bool RoleExists(int roleId)
        {
            return _context.Roles.Any(r => r.RoleID == roleId);
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool UpdateRole(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
