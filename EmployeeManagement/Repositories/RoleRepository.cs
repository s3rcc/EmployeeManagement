using EmployeeManagement.Data;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private DataContext _context;
        public RoleRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public bool CreateRole(Role role)
        {
                _context.Roles.Add(role);
                _context.SaveChanges();
                return true;
        }

        public bool DeleteRole(Role role)
        {
                _context.Roles.Remove(role);
                _context.SaveChanges();
                return true;
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

        public bool UpdateRole(Role role)
        {
                _context.Roles.Update(role);
                _context.SaveChanges();
                return true;
        }
    }
}
