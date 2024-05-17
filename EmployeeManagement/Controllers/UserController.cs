using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("/api/[controller]/GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetUsers();
            if (users == null)
            {
                return BadRequest("No user found!");
            }
            return Ok(users);
        }


        [HttpGet]
        [Route("/api/[controller]/GetUserById")]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                var user = _userService.GetUserById(userId);
                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound($"No user with type ID: {userId}!");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }


        [HttpGet]
        [Route("/api/[controller]/GetUserByName")]
        public IActionResult GetUserByName(string name)
        {
            try
            {
                var user = _userService.GetUserName(name);
                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        [Route("/api/[controller]/AddNewUser")]
        public IActionResult AddNewUser([FromBody] UserDTO userDto)
        {
            try
            {
                _userService.AddUser(userDto);
                var response = new
                {
                    Message = "User added successfully!",
                    AddedUser = userDto
                };
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // User already exists
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }


        [HttpPut]
        [Route("/api/[controller]/UpdateUser")]
        public IActionResult UpdateUser([FromBody] UserDTO userDto)
        {
            try
            {
                _userService.UpdateUser(userDto);
                var response = new
                {
                    Message = "User updated successfully!",
                    UpdatedUser = userDto
                };
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // User does not exist
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpDelete]
        [Route("/api/[controller]/DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok("User deleted successfully!");
            }
            catch (InvalidOperationException ex)
            {
                // User does not exist
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

    }

}

