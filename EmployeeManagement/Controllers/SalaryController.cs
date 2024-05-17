using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Authorize (Policy = "ManagerPolicy")]
    [Authorize (Policy = "AdminPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryService _salaryService;
        public SalaryController(ISalaryService salaryService)
        {
            _salaryService = salaryService;
        }
        [HttpGet]
        [Route("/api/[controller]/GetAllSalarys")]
        public IActionResult GetAllSalarys()
        {
            var salarys = _salaryService.GetAllSalarys();
            if (salarys == null)
            {
                return BadRequest("No salary found!");
            }
            return Ok(salarys);
        }


        [HttpGet]
        [Route("/api/[controller]/GetSalaryById")]
        public IActionResult GetSalaryById(int salaryId)
        {
            try
            {
                var salary = _salaryService.GetSalaryById(salaryId);
                return Ok(salary);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound($"No salary with type ID: {salaryId}!");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("/api/[controller]/AddNewSalary")]
        public IActionResult AddNewSalary([FromBody] SalaryDTO salaryDto)
        {
            try
            {
                _salaryService.AddSalary(salaryDto);
                var response = new
                {
                    Message = "Salary added successfully!",
                    AddedSalary = salaryDto
                };
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // Salary already exists
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }


        [HttpPut]
        [Route("/api/[controller]/UpdateSalary")]
        public IActionResult UpdateSalary([FromBody] SalaryDTO salaryDto)
        {
            try
            {
                _salaryService.UpdateSalary(salaryDto);
                var response = new
                {
                    Message = "Salary updated successfully!",
                    UpdatedSalary = salaryDto
                };
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // Salary does not exist
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpDelete]
        [Route("/api/[controller]/DeleteSalary")]
        public IActionResult DeleteSalary(int id)
        {
            try
            {
                _salaryService.DeleteSalary(id);
                return Ok("Salary deleted successfully!");
            }
            catch (InvalidOperationException ex)
            {
                // Salary does not exist
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
