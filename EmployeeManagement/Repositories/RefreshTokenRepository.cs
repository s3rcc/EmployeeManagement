using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.RepositoryInterfaces;

namespace EmployeeManagement.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly DataContext _context;
        public RefreshTokenRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateRefreshToken(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
            _context.SaveChanges();
            return true;
        }

        public RefreshToken GetRefreshToken(int id)
        {
            return _context.RefreshTokens.Where(x => x.ID == id).FirstOrDefault();
        }

        public ICollection<RefreshToken> GetRefreshTokens()
        {
            return _context.RefreshTokens.OrderBy(x => x.ID).ToList();
        }

        public RefreshToken GetRefreshTokenByToken(string token)
        {
            return _context.RefreshTokens.FirstOrDefault(rt => rt.Token == token);
        }
    }
}
