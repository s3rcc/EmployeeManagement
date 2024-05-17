using EmployeeManagement.Models;

namespace EmployeeManagement.RepositoryInterfaces
{
    public interface IRefreshTokenRepository
    {
        ICollection<RefreshToken> GetRefreshTokens();
        RefreshToken GetRefreshToken(int id);
        bool CreateRefreshToken(RefreshToken refreshToken);
        public RefreshToken GetRefreshTokenByToken(string token);
    }
}
