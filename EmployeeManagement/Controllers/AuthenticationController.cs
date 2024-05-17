using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("/api/[controller]/Login")]
        public IActionResult Login(RequestLoginDTO requestLogin)
        {
            try
            {
                var response = _authenticationService.Authenticator(requestLogin);
                return Ok(response);
            }
            catch (AuthenticationException ex)
            {
                return Unauthorized(ex.Message); 
            }
            catch (Exception ex)
            {
                return BadRequest( ex.Message);
            }
        }
        [HttpPost]
        [Route("/api/[controller]/Register")]
        public IActionResult Register([FromBody] RequestRegisterDTO registerDto)
        {
            try
            {
                var response = _authenticationService.Register(registerDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
