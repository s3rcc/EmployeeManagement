using EmployeeManagement.Dto;



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
        void DeleteUser(int userId);
    }
}