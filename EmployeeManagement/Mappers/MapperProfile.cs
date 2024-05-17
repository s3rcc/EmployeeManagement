using AutoMapper;
using EmployeeManagement.Dto;
using EmployeeManagement.Models;

namespace EmployeeManagement.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<FormType, FormTypeDTO>().ReverseMap();
            CreateMap<Claim, ClaimDTO>().ReverseMap();
            CreateMap<FormType, UserDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Salary, SalaryDTO>().ReverseMap();
            CreateMap<Form, FormDTO>().ReverseMap();
            CreateMap<Form, AddFormDTO>().ReverseMap();
            CreateMap<Form, UpdateFormDTO>().ReverseMap();
            CreateMap<UserClaim, UserClaimDTO>().ReverseMap();
        }
    }
}
