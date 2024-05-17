namespace EmployeeManagement.Dto
{
    public class UpdateUserClaimsDTO
    {
        public int UserID { get; set; }
        public ICollection<UserClaimDTO> UserClaims { get; set; }
    }
}
