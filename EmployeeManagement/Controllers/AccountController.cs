using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "AdminPolicy")]
    [Authorize(Policy = "EmployeePolicy")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPut]
        [Route("/api/[controller]/UpdateUserBasic")]
        public IActionResult UpdateUser([FromBody] UserBasicDTO userDto)
        {
            try
            {
                _userService.UpdateUserBasic(userDto);
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

    }
}
