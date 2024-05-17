using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Authorize(Policy = "ManagerPolicy")]
    [Authorize(Policy = "AdminPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class FormTypeController : ControllerBase   
    {
        private readonly IFormTypeService _formTypeService;
        public FormTypeController(IFormTypeService formTypeService)
        {
            _formTypeService = formTypeService;
        }


        [HttpGet]
        [Route("/api/[controller]/GetAllFormTypes")]
        public IActionResult GetAllFormTypes()
        {
            try
            {
                var formTypes = _formTypeService.GetFormTypes();
                return Ok(formTypes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("/api/[controller]/GetFormTypeById")]
        public IActionResult GetFormTypeById(int id)
        {
            try
            {
                var formType = _formTypeService.GetFormById(id);
                return Ok(formType);
            }
            catch(InvalidOperationException ex)
            {
                return NotFound($"No form type with type ID: {id}!");
            }
            catch(Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }

        }


        [HttpGet]
        [Route("/api/[controller]/GetAllFormTypeByName")]
        public IActionResult GetFormTypeByName(string name)
        {
            try
            {
                var formType = _formTypeService.GetFormByName(name);
                    return Ok(formType);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        [HttpPost]
        [Route("/api/[controller]/AddNewFormType")]
        public IActionResult AddNewFormType([FromBody] FormTypeDTO formTypeDto)
        {
            try
            {
                _formTypeService.AddFormType(formTypeDto);
                var response = new
                {
                    Message = "Form type added successfully!",
                    AddedFormType = formTypeDto
                };
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // Form type already exists
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPut]
        [Route("/api/[controller]/UpdateFormType")]
        public IActionResult UpdateFormType([FromBody] FormTypeDTO formTypeDto)
        {
            try
            {
                _formTypeService.UpdateFormType(formTypeDto);
                var response = new
                {
                    Message = "Form type updated successfully!",
                    UpdatedFormType = formTypeDto
                };
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // Form type does not exist
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpDelete]
        [Route("/api/[controller]/DeleteFormType")]
        public IActionResult DeleteFormType(int id)
        {
            try
            {
                _formTypeService.DeleteFormType(id);
                return Ok("Form type deleted successfully!");
            }
            catch (InvalidOperationException ex)
            {
                // Form type does not exist
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
