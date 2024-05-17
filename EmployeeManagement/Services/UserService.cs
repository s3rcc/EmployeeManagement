using AutoMapper;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using EmployeeManagement.RepositoryInterfaces;

namespace EmployeeManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public void AddUser(UserDTO userDto)
        {
            //Check valid data
            if (userDto == null
                || string.IsNullOrWhiteSpace(userDto.Name)
                || string.IsNullOrWhiteSpace(userDto.Email)
                || string.IsNullOrWhiteSpace(userDto.Phone)
                || userDto.RoleID == 0)
            {
                throw new ArgumentException("User data is invalid.");
            }
            //Check if the role ID input exists
            if (!_roleRepository.RoleExists(userDto.RoleID))
            {
                throw new InvalidOperationException($"Role ID {userDto.RoleID} does not exist!");
            }
            //Check user exists
            if (_userRepository.UserExists(userDto.UserID))
            {
                throw new InvalidOperationException($"User with ID {userDto.UserID} already exists!");
            }
            //Create a new user
            var newUser = _mapper.Map<Models.User>(userDto);
            _userRepository.CreateUser(newUser);
        }

        public void DeleteUser(int userId)
        {
            var user = _userRepository.GetUser(userId);
            //Check if the user exists
            if (user == null)
            {
                throw new InvalidOperationException($"user with ID {userId} does not exist!");
            }
            //Process to delete the user
            _userRepository.DeleteUser(user);
        }

        public UserDTO GetUserById(int id)
        {
            var user = _userRepository.GetUser(id);
            if (user == null)
            {
                throw new InvalidOperationException($"User with ID {id} does not exist.");
            }
            return _mapper.Map<UserDTO>(user);
        }

        public UserDTO GetUserByUserName(string name)
        {
            var user = _userRepository.GetUserByUserName(name);
            if (user == null)
            {
                throw new InvalidOperationException($"Not Found");
            }
            return _mapper.Map<UserDTO>(user);
        }

        public ICollection<UserDTO> GetUserName(string name)
        {
            var users = _userRepository.GetUserName(name);
            if (users == null)
            {
                throw new InvalidOperationException($"No result found!");
            }
            return _mapper.Map<ICollection<UserDTO>>(users);
        }

        public ICollection<UserDTO> GetUsers()
        {
            var users = _userRepository.GetUsers();
            if (users == null)
            {
                throw new InvalidOperationException($"No result found!");
            }
            return _mapper.Map<ICollection<UserDTO>>(users);
        }

        public void UpdateUser(UserDTO userDto)
        {
            //Check valid data
            if (userDto == null
                || string.IsNullOrWhiteSpace(userDto.Name)
                || string.IsNullOrWhiteSpace(userDto.Email)
                || string.IsNullOrWhiteSpace(userDto.Phone)
                || userDto.RoleID == 0)
            {
                throw new ArgumentException("User data is invalid.");
            }
            //Check if the role ID input exists
            if (!_roleRepository.RoleExists(userDto.RoleID))
            {
                throw new InvalidOperationException($"Role ID {userDto.RoleID} does not exist!");
            }
            //Check user exists
            if (!_userRepository.UserExists(userDto.UserID))
            {
                throw new InvalidOperationException($"User with ID {userDto.UserID} does not exist!");
            }
            var updateUser = _mapper.Map<Models.User>(userDto);
            _userRepository.UpdateUser(updateUser);
        }
    }
}
