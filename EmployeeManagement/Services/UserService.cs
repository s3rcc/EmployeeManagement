using AutoMapper;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using EmployeeManagement.RepositoryInterfaces;
using System.Security.Claims;

namespace EmployeeManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IClaimRepository _claimRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper, IClaimRepository claimRepository, IHttpContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _claimRepository = claimRepository;
            _contextAccessor = contextAccessor;
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

        public ICollection<UserClaimDTO> GetUserClaimsByUserId(int userId)
        {
            var userClaims = _userRepository.GetUserClaimsByUserId(userId);
            if (!userClaims.Any())
            {
                throw new Exception("User claims not found.");
            }

            var userClaimDtos = new List<UserClaimDTO>();
            foreach (var userClaim in userClaims)
            {
                var claimType = _claimRepository.GetClaimTypeById(userClaim.ClaimID);
                userClaimDtos.Add(new UserClaimDTO
                {
                    ClaimID = userClaim.ClaimID,
                    ClaimType = claimType,
                    IsClaim = userClaim.IsClaim
                });
            }

            return userClaimDtos;
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

            // Retrieve the user from the repository
            var existingUser = _userRepository.GetUser(userDto.UserID);

            //Check user exists
            if (existingUser == null)
            {
                throw new InvalidOperationException($"User with ID {userDto.UserID} does not exist!");
            }

            // Update specific properties of the existing user
            existingUser.Name = userDto.Name;
            existingUser.Email = userDto.Email;
            existingUser.Phone = userDto.Phone;
            existingUser.RoleID = userDto.RoleID;

            // Save the updated user to the repository
            _userRepository.UpdateUser(existingUser);
        }

        public void UpdateUserClaims(int userId, IEnumerable<UserClaimDTO> userClaims)
        {
            var existingClaims = _userRepository.GetUserClaimsByUserId(userId).ToList();
            if (!existingClaims.Any())
            {
                throw new Exception("No user claims found to update.");
            }

            foreach (var userClaimDto in userClaims)
            {
                var existingClaim = existingClaims.FirstOrDefault(uc => uc.ClaimID == userClaimDto.ClaimID);
                if (existingClaim != null)
                {
                    existingClaim.IsClaim = userClaimDto.IsClaim;
                }
            }
            _userRepository.UpdateUserClaims(existingClaims);
        }

        public int GetUserIdFromToken()
        {
            return int.Parse(_contextAccessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value);
        }

        public void UpdateUserBasic(UserBasicDTO userDto)
        {
            // Check valid data
    if (userDto == null
        || string.IsNullOrWhiteSpace(userDto.Name)
        || string.IsNullOrWhiteSpace(userDto.Email)
        || string.IsNullOrWhiteSpace(userDto.Phone))
            {
                throw new ArgumentException("User data is invalid.");
            }

            var currentUserId = GetUserIdFromToken();
            //Check user exists
            if (!_userRepository.UserExists(currentUserId))
            {
                throw new InvalidOperationException($"Session expire");
            }

            // Retrieve the current user from the repository
            var currentUser = _userRepository.GetUser(currentUserId);

            // Update only the non-password properties
            currentUser.Name = userDto.Name;
            currentUser.Email = userDto.Email;
            currentUser.Phone = userDto.Phone;

            // Save the updated user to the repository
            _userRepository.UpdateUser(currentUser);
        }
    }
}
