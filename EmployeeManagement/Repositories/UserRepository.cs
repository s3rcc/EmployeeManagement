using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories
{
    public class UserRepository : IUserRepository
    {
        DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public bool CreateUserClaim(UserClaim userClaim)
        {
            _context.UserClaims.Add(userClaim);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

        public ICollection<UserClaim> GetUserClaimsByUserId(int id)
        {
            return _context.UserClaims.Where(x => x.UserID == id).ToList();
        }

        public User GetUser(int id)
        {
            return _context.Users.Where(x => x.UserID == id).FirstOrDefault();  
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.Where(x => x.Email == email).FirstOrDefault();
        }

        public User GetUserByRefreshToken(string refreshToken)
        {
            return _context.Users.Where(x => x.RefreshTokens.Any(t => t.Token == refreshToken)).FirstOrDefault();
        }

        public User GetUserByUserName(string userName)
        {
            return _context.Users.Include(u => u.Role).FirstOrDefault(x => x.UserName == userName);
        }

        public ICollection<User> GetUserName(string name)
        {
            return _context.Users.Where(x => x.Name.Contains(name)).ToList();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(x => x.UserID).ToList();
        }

        public bool UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateUserClaims(ICollection<UserClaim> userClaims)
        {
            _context.UserClaims.UpdateRange(userClaims);
            _context.SaveChanges();
            return true;

        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(x => x.UserID == userId);
        }
    }
}
