using AutoMapper;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Repositories;

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
            //Check claim exists
            if (_userRepository.UserExists(userDto.UserID))
            {
                throw new InvalidOperationException($"User with ID {userDto.UserID} already exists!");
            }
            //Create a new claim
            var newUser = _mapper.Map<Models.User>(userDto);
            _userRepository.CreateUser(newUser);
        }

        public void DeleteUser(int userId)
        {
            var claim = _userRepository.GetUser(userId);
            //Check if the claim exists
            if (claim == null)
            {
                throw new InvalidOperationException($"claim with ID {userId} does not exist!");
            }
            //Process to delete the claim
            _userRepository.DeleteUser(claim);
        }

        public UserDTO GetUserById(int id)
        {
            var claim = _userRepository.GetUser(id);
            if (claim == null)
            {
                throw new InvalidOperationException($"User with ID {id} does not exist.");
            }
            return _mapper.Map<UserDTO>(claim);
        }

        public ICollection<UserDTO> GetUserName(string name)
        {
            var claims = _userRepository.GetUserName(name);
            if (claims == null)
            {
                throw new InvalidOperationException($"No result found!");
            }
            return _mapper.Map<ICollection<UserDTO>>(claims);
        }

        public ICollection<UserDTO> GetUsers()
        {
            var claims = _userRepository.GetUsers();
            if (claims == null)
            {
                throw new InvalidOperationException($"No result found!");
            }
            return _mapper.Map<ICollection<UserDTO>>(claims);
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
            //Check claim exists
            if (!_userRepository.UserExists(userDto.UserID))
            {
                throw new InvalidOperationException($"User with ID {userDto.UserID} does not exist!");
            }
            var updateUser = _mapper.Map<Models.User>(userDto);
            _userRepository.UpdateUser(updateUser);
        }
    }
}
