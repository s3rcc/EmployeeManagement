using EmployeeManagement.Models;

namespace EmployeeManagement.RepositoryInterfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        ICollection<User> GetUserName(string name);

        User GetUserByUserName(string userName);
        User GetUserByEmail(string email);
        bool UserExists(int userId);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        User GetUserByRefreshToken(string refreshToken);
        public bool CreateUserClaim(UserClaim userClaim);
        ICollection<UserClaim> GetUserClaimsByUserId(int id);
        bool UpdateUserClaims(ICollection<UserClaim> userClaims);
    }
}
