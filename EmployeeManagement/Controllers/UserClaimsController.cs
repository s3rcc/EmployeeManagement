using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using EmployeeManagement.RepositoryInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class UserClaimsController : Controller
    {
        private readonly IUserService _userService;
        public UserClaimsController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet()]
        [Route("/api/[controller]/GetUserClaims/{userId}")]
        public ActionResult<IEnumerable<UserClaimDTO>> GetUserClaims(int userId)
        {
            try
            {
                var userClaims = _userService.GetUserClaimsByUserId(userId);
                return Ok(userClaims);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


        [HttpPut]
        [Route("/api/[controller]/UpdateUserClaims")]
        public ActionResult UpdateUserClaims([FromBody] UpdateUserClaimsDTO updateUserClaimsDto)
        {
            try
            {
                _userService.UpdateUserClaims(updateUserClaimsDto.UserID, updateUserClaimsDto.UserClaims);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
