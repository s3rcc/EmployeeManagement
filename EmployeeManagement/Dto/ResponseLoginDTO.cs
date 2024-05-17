namespace EmployeeManagement.Dto
{
    public class ResponseLoginDTO
    {
        public string Name { get; set; }
        public int UserID { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }

    }
}
