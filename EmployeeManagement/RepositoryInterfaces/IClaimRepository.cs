using EmployeeManagement.Models;

namespace EmployeeManagement.RepositoryInterfaces
{
    public interface IClaimRepository
    {
        ICollection<Claim> GetClaims();
        Claim GetClaim(int id);
        ICollection<Claim> GetClaimName(string name);
        bool ClaimExists(int id);
        bool CreateClaim(Claim claim);
        bool UpdateClaim(Claim claim);
        bool DeleteClaim(Claim claim);
    }
}
