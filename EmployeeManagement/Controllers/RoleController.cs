using AutoMapper;
using EmployeeManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Route("/api/[controller]/GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            var roles = _roleService.GetAllRoles();
            if(roles == null)
            {
                return NotFound("No role");
            }
            return Ok(roles);
        }
    }
}
