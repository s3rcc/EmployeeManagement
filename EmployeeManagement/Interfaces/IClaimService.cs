using EmployeeManagement.Dto;

namespace EmployeeManagement.Interfaces
{
    public interface IClaimService
    {
        ICollection<ClaimDTO> GetClaims();
        ClaimDTO GetClaimById(int id);
        ICollection<ClaimDTO> GetClaimByName(string name);
        void AddClaim(ClaimDTO claimDto);
        void UpdateClaim(ClaimDTO claimDto);
        void DeleteClaim(int claimId);
    }
}
