﻿

namespace EmployeeManagement.Dto
{
    public class RequestRegisterDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public int RoleID { get; set; }
    }
}
