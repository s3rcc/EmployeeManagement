using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class ClaimController : ControllerBase
    {
        private readonly IClaimService _claimService;
        public ClaimController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        [HttpGet]
        [Route("/api/[controller]/GetAllClaims")]
        public IActionResult GetAllClaims() 
        {
            var claims = _claimService.GetClaims();
            if (claims == null)
            {
                return BadRequest("No claim found!");
            }
            return Ok(claims);
        }


        [HttpGet]
        [Route("/api/[controller]/GetClaimById")]
        public IActionResult GetClaimById(int claimId) 
        {
            try
            {
                var claim = _claimService.GetClaimById(claimId);
                return Ok(claim);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound($"No claim with type ID: {claimId}!");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }


        [HttpGet]
        [Route("/api/[controller]/GetClaimByName")]
        public IActionResult GetClaimByName(string name) 
        {
            try
            {
                var claim = _claimService.GetClaimByName(name);
                return Ok(claim);
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
        [Route("/api/[controller]/AddNewClaim")]
        public IActionResult AddNewClaim([FromBody] ClaimDTO claimDto) 
        {
            try
            {
                _claimService.AddClaim(claimDto);
                var response = new
                {
                    Message = "Claim added successfully!",
                    AddedClaim = claimDto
                };
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // Claim already exists
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            
        }


        [HttpPut]
        [Route("/api/[controller]/UpdateClaim")]
        public IActionResult UpdateClaim([FromBody] ClaimDTO claimDto)
        {
                try
                {
                    _claimService.UpdateClaim(claimDto);
                    var response = new
                    {
                        Message = "Claim updated successfully!",
                        UpdatedClaim = claimDto
                    };
                    return Ok(response);
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    // Claim does not exist
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred: {ex.Message}");
                }
            }


        [HttpDelete]
        [Route("/api/[controller]/DeleteClaim")]
        public IActionResult DeleteClaim(int id)
        {
                try
                {
                    _claimService.DeleteClaim(id);
                    return Ok("Claim deleted successfully!");
                }
                catch (InvalidOperationException ex)
                {
                    // Claim does not exist
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return BadRequest($"An error occurred: {ex.Message}");
                }
            }

    }
}
