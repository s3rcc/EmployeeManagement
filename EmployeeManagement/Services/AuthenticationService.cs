using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using EmployeeManagement.RepositoryInterfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ISalaryRepository _salaryRepository;
    private readonly IClaimRepository _claimRepository;
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(
        IRefreshTokenRepository refreshTokenRepository,
        IConfiguration configuration,
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        ILogger<AuthenticationService> logger,
        ISalaryRepository salaryRepository,
        IClaimRepository claimRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _configuration = configuration;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _logger = logger;
        _salaryRepository = salaryRepository;
        _claimRepository = claimRepository;
    }

    public ResponseLoginDTO Authenticator(RequestLoginDTO requestLogin)
    {
        _logger.LogInformation("Authenticate method called with UserName: {UserName}", requestLogin.UserName);

        var user = _userRepository.GetUserByUserName(requestLogin.UserName);
        if (user == null || !BCrypt.Net.BCrypt.Verify(requestLogin.Password, user.PasswordHash))
        {
            _logger.LogWarning("Invalid username or password for UserName: {UserName}", requestLogin.UserName);
            throw new AuthenticationException("Invalid username or password.");
        }

        var token = GenerateJwtToken(user);
        var refreshToken = GenerateRefreshToken(user);

        _logger.LogInformation("User authenticated successfully. UserID: {UserID}", user.UserID);

        return new ResponseLoginDTO
        {
            Name = user.Name,
            UserID = user.UserID,
            Token = token,
            RefreshToken = refreshToken.Token
        };
    }

    public ResponseLoginDTO Register(RequestRegisterDTO register)
    {
        if (_userRepository.GetUserByUserName(register.UserName) != null)
        {
            throw new Exception("Username already exists.");
        }

        if (_userRepository.GetUserByEmail(register.Email) != null)
        {
            throw new Exception("Email already exists.");
        }

        var role = _roleRepository.GetRole(register.RoleID);
        if (role == null)
        {
            throw new Exception("Invalid role ID.");
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(register.PasswordHash);

        var user = new User
        {
            UserName = register.UserName,
            Email = register.Email,
            Name = register.Name,
            PasswordHash = passwordHash,
            Phone = register.Phone,
            RoleID = register.RoleID,
        };
        _userRepository.CreateUser(user);

        var claims = _claimRepository.GetClaimsByRoleId(register.RoleID);
        foreach (var claim in claims)
        {
            var userClaim = new UserClaim
            {
                UserID = user.UserID,
                ClaimID = claim.ClaimID,
                IsClaim = true
            };
            _userRepository.CreateUserClaim(userClaim);
        }

        var salary = new Salary
        {
            BaseSalary = 0,
            DailyRate = 0,
            User = user
        };

        _salaryRepository.CreateSalary(salary);

        user.SalaryID = salary.SalaryID;
        _userRepository.UpdateUser(user);

        var token = GenerateJwtToken(user);
        var refreshToken = GenerateRefreshToken(user);

        return new ResponseLoginDTO
        {
            Name = user.Name,
            UserID = user.UserID,
            Token = token,
            RefreshToken = refreshToken.Token
        };
    }

    public ResponseLoginDTO RefreshToken(string refreshToken)
    {
        _logger.LogInformation("RefreshToken method called with refreshToken: {RefreshToken}", refreshToken);

        if (!ValidateRefreshToken(refreshToken))
        {
            _logger.LogWarning("Invalid refresh token.");
            throw new AuthenticationException("Invalid refresh token.");
        }

        var user = _userRepository.GetUserByRefreshToken(refreshToken);

        _logger.LogInformation("User retrieved for refresh token. UserID: {UserID}", user.UserID);

        var token = GenerateJwtToken(user);
        var newRefreshToken = GenerateRefreshToken(user);

        return new ResponseLoginDTO
        {
            Name = user.Name,
            UserID = user.UserID,
            Token = token,
            RefreshToken = newRefreshToken.Token
        };
    }

    private RefreshToken GenerateRefreshToken(User user)
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);

        var refreshToken = new RefreshToken
        {
            UserID = user.UserID,
            Expire = DateTime.UtcNow.AddDays(1),
            IsActive = true,
            Token = token
        };

        _refreshTokenRepository.CreateRefreshToken(refreshToken);
        return refreshToken;
    }

    private bool ValidateRefreshToken(string refreshToken)
    {
        _logger.LogInformation("Validating refresh token: {RefreshToken}", refreshToken);

        var refreshTokenEntity = _refreshTokenRepository.GetRefreshTokenByToken(refreshToken);
        return refreshTokenEntity != null && refreshTokenEntity.IsActive && refreshTokenEntity.Expire > DateTime.UtcNow;
    }

    private string GenerateJwtToken(User user)
    {
        _logger.LogInformation("Generating JWT token for user: {UserID}", user.UserID);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);
        var expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpiresTimeInMinutes"]));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] 
            {
                new System.Security.Claims.Claim(ClaimTypes.Name, user.UserName),
                new System.Security.Claims.Claim(ClaimTypes.Email, user.Email),
                new System.Security.Claims.Claim(ClaimTypes.Role, user.Role.RoleName),
                new System.Security.Claims.Claim(ClaimTypes.NameIdentifier, user.UserID.ToString())
            }),
            Expires = expires,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
