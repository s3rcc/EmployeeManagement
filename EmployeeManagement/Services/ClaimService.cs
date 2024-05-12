using AutoMapper;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using System.Security.Claims;

namespace EmployeeManagement.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IMapper _mapper;
        private readonly IClaimRepository _claimRepository;
        private readonly IRoleRepository _roleRepository;
        public ClaimService(IMapper mapper, IClaimRepository claimRepository, IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _claimRepository = claimRepository;
            _roleRepository = roleRepository;
        }

        public void AddClaim(ClaimDTO claimDto)
        {
            //Check valid data
            if (claimDto == null || string.IsNullOrWhiteSpace(claimDto.ClaimType) || claimDto.RoleID == 0)
            {
                throw new ArgumentException("Claim data is invalid.");
            }
            //Check if the role ID input exists
            if (!_roleRepository.RoleExists(claimDto.RoleID))
            {
                throw new InvalidOperationException($"Role ID {claimDto.RoleID} does not exist!");
            }
            //Check claim exists
            if (_claimRepository.ClaimExists(claimDto.ClaimID))
            {
                throw new InvalidOperationException($"Claim with ID {claimDto.ClaimID} already exists!");
            }
            //Create a new claim
            var newClaim = _mapper.Map<Models.Claim>(claimDto);
            _claimRepository.CreateClaim(newClaim);
        }

        public void DeleteClaim(int claimId)
        {
            var claim = _claimRepository.GetClaim(claimId);
            //Check if the claim exists
            if (claim == null)
            {
                throw new InvalidOperationException($"claim with ID {claimId} does not exist!");
            }
            //Process to delete the claim
            _claimRepository.DeleteClaim(claim);
        }

        public ICollection<ClaimDTO> GetClaimByName(string name)
        {
            var claims = _claimRepository.GetClaimName(name);
            if (claims == null)
            {
                throw new InvalidOperationException($"No result found!");
            }
            return _mapper.Map<ICollection<ClaimDTO>>(claims);
        }

        public ClaimDTO GetClaimById(int id)
        {
            var claim = _claimRepository.GetClaim(id);
            if (claim == null)
            {
                throw new InvalidOperationException($"Claim with ID {id} does not exist.");
            }
            return _mapper.Map<ClaimDTO>(claim);
        }

        public ICollection<ClaimDTO> GetClaims()
        {
            var claims = _claimRepository.GetClaims();
            if (claims == null)
            {
                throw new InvalidOperationException($"No result found!");
            }
            return _mapper.Map<ICollection<ClaimDTO>>(claims);
        }

        public void UpdateClaim(ClaimDTO claimDto)
        {
            //Check valid data
            if (claimDto == null || string.IsNullOrWhiteSpace(claimDto.ClaimType) || claimDto.RoleID == 0)
            {
                throw new ArgumentException("Claim data is invalid.");
            }
            //Check if the role ID input exists
            if(!_roleRepository.RoleExists(claimDto.RoleID))
            {
                throw new InvalidOperationException($"Role ID {claimDto.RoleID} does not exist!");
            }
            //Check claim exists
            if (!_claimRepository.ClaimExists(claimDto.ClaimID))
            {
                throw new InvalidOperationException($"Claim with ID {claimDto.ClaimID} does not exist!");
            }
            var updateClaim = _mapper.Map<Models.Claim>(claimDto);
            _claimRepository.UpdateClaim(updateClaim);
        }
    }
}
