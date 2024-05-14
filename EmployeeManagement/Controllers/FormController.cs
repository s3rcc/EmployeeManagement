using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormController : ControllerBase
    {
        private readonly IFormService _formService;
        public FormController(IFormService formService)
        {
            _formService = formService;
        }

        [HttpGet]
        [Route("/api/[controller]/GetAllForms")]
        public IActionResult GetAllForms()
        {
            var forms = _formService.GetForms();
            if (forms == null)
            {
                return BadRequest("No form found!");
            }
            return Ok(forms);
        }


        [HttpGet("{id}")]
        public IActionResult GetFormById(int id)
        {
            try
            {
                var form = _formService.GetFormById(id);
                return Ok(form);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet]
        [Route("/api/[controller]/GetFormByName")]
        public IActionResult GetFormByName(string name)
        {
            try
            {
                var form = _formService.GetFormByName(name);
                return Ok(form);
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
        [Route("/api/[controller]/GetFormByTypeId")]
        public IActionResult GetFormByTypeId(int id)
        {
            try
            {
                var form = _formService.GetFormByTypeId(id);
                return Ok(form);
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
        [Route("/api/[controller]/GetFormByUserId")]
        public IActionResult GetFormByUserId(int id)
        {
            try
            {
                var form = _formService.GetFormByUserId(id);
                return Ok(form);
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
        public async Task<IActionResult> AddNewForm([FromForm] FormDTO formDto, [FromForm] List<IFormFile> attachments)
        {
            try
            {
                var attachmentData = await ConvertFilesToBase64(attachments);
                _formService.AddForm(formDto, attachmentData);
                return Ok("Form created successfully.");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateForm(int id, [FromForm] FormDTO formDto, [FromForm] List<IFormFile> attachments)
        {
            try
            {
                var attachmentData = await ConvertFilesToBase64(attachments);
                _formService.UpdateForm(id, formDto, attachmentData);
                return Ok("Form updated successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteForm(int id)
        {
            try
            {
                _formService.DeleteForm(id);
                return Ok("Form deleted successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("{id}/attachments")]
        public IActionResult GetAttachments(int id)
        {
            try
            {
                var attachments = _formService.GetAttachments(id);
                return Ok(attachments);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPost("{id}/attachments")]
        public async Task<IActionResult> AddAttachments(int id, [FromForm] List<IFormFile> attachments)
        {
            try
            {
                var attachmentData = await ConvertFilesToBase64(attachments);
                _formService.AddAttachments(id, attachmentData);
                return Ok("Attachments added successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private async Task<List<byte[]>> ConvertFilesToBase64(List<IFormFile> files)
        {
            var base64Files = new List<byte[]>();
            foreach (var file in files)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();
                    base64Files.Add(fileBytes);
                }
            }
            return base64Files;
        }
    }
}
