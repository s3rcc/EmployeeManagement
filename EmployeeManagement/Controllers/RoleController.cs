using AutoMapper;
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
            var roles = _roleService.GetAllRoles();
            if (roles == null)
            {
                return NotFound("No role");
            }
            return Ok(roles);
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
                return NotFound($"No role with role ID: {id}!");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("/api/[controller]/AddNewRole")]
        public IActionResult AddNewRow([FromBody] RoleDTO roleDto)
        {
            try
            {
                _roleService.AddRole(roleDto);
                return Ok("Role added successfully.");
            }
            catch (InvalidOperationException ex)
            {
                // Role already exists
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                // Log exception or return appropriate error response
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
                return Ok("Role updated successfully.");
            }
            catch(InvalidOperationException ex)
            {
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
                return Ok("Role deleted successfully.");
            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
