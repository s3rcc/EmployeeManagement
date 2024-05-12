using EmployeeManagement.Dto;

namespace EmployeeManagement.Interfaces
{
    public interface IUserService
    {
        ICollection<UserDTO> GetUsers();
        UserDTO GetUserById(int id);
        ICollection<UserDTO> GetUserName(string name);
        void AddUser(UserDTO userDto);
        void UpdateUser(UserDTO userDto);
        void DeleteUser(int userId);
    }
}
