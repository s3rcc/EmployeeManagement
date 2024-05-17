using EmployeeManagement.Dto;
using EmployeeManagement.Models;



namespace EmployeeManagement.Interfaces
{
    public interface IUserService
    {
        ICollection<UserDTO> GetUsers();
        UserDTO GetUserById(int id);
        UserDTO GetUserByUserName(string name);
        ICollection<UserDTO> GetUserName(string name);
        void AddUser(UserDTO userDto);
        void UpdateUser(UserDTO userDto);
        void UpdateUserBasic(UserBasicDTO userDto);
        void DeleteUser(int userId);
        ICollection<UserClaimDTO> GetUserClaimsByUserId(int userId);
        void UpdateUserClaims(int userId, IEnumerable<UserClaimDTO> userClaims);
    }
}