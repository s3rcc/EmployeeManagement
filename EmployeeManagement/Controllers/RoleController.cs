using EmployeeManagement.Dto;
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
            try
            {
                var roles = _roleService.GetAllRoles();
                return Ok(roles);
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


        [HttpGet]
        [Route("/api/[controller]/GetRoleById")]
        public IActionResult GetRoleById(int id)
        {
            try
            {
                var role = _roleService.GetRoleById(id);
                return Ok(role);
            }
            catch (InvalidOperationException ex)
            {
                // Role does not exist
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("/api/[controller]/AddNewRole")]
        public IActionResult AddNewRole([FromBody] RoleDTO roleDto)
        {
            try
            {
                //Check isEmpty
                if (roleDto == null)
                {
                    return BadRequest("Fields are empty");
                }
                _roleService.AddRole(roleDto);
                var response = new
                {
                    Message = "Role added successfully!",
                    AddedRole = roleDto
                };
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                //Validate input
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // Role already exists
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPut]
        [Route("/api/[controller]/UpdateRole")]
        public IActionResult UpdateRole([FromBody] RoleDTO roleDto)
        {
            try
            {
                _roleService.UpdateRole(roleDto);
                var response = new
                {
                    Message = "Role updated successfully!",
                    UpdatedRole = roleDto
                };
                return Ok(response);
            }
            catch(ArgumentException ex)
            {
                //Validate input
                return BadRequest(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                // Role does not exist
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpDelete]
        [Route("/api/[controller]/DeleteRole")]
        public IActionResult DeleteRole(int roleId)
        {
            try
            {
                _roleService.DeleteRole(roleId);
                return Ok("Role deleted successfully!");
            }
            catch(InvalidOperationException ex)
            {
                // Role does not exist
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
