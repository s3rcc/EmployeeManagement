using EmployeeManagement.Dto;

namespace EmployeeManagement.Interfaces
{
    public interface IAuthenticationService
    {
        public ResponseLoginDTO Authenticator(RequestLoginDTO requestLogin);
        public ResponseLoginDTO Register(RequestRegisterDTO register);
    }
}
