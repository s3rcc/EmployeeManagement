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
            try
            {
                _context.Roles.Add(role);
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
            return false;
            };
        }

        public bool DeleteRole(Role role)
        {
            try
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("Error occurred while deleting role.", ex);
            }
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
            try
            {
                _context.Roles.Update(role);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating role.", ex);
            }
        }
    }
}
