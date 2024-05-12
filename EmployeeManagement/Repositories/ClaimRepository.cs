using EmployeeManagement.Data;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.Repositories
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly DataContext _context;
        public ClaimRepository(DataContext context)
        {
            _context = context;
        }

        public bool ClaimExists(int id)
        {
            return _context.Claims.Any(x => x.ClaimID == id);
        }

        public bool CreateClaim(Claim claim)
        {
            _context.Claims.Add(claim);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteClaim(Claim claim)
        {
            _context.Claims.Remove(claim);
            _context.SaveChanges();
            return true;
        }

        public Claim GetClaim(int id)
        {
            return _context.Claims.Where(x => x.ClaimID == id).FirstOrDefault();
        }

        public ICollection<Claim> GetClaimName(string name)
        {
            return _context.Claims.Where(x => x.ClaimType.Contains(name)).ToList();
        }

        public ICollection<Claim> GetClaims()
        {
            return _context.Claims.OrderBy(x => x.ClaimID).ToList();
        }

        public bool UpdateClaim(Claim claim)
        {
            _context.Claims.Update(claim);
            _context.SaveChanges();
            return true;
        }
    }
}
