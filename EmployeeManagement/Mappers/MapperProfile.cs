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
        }
    }
}
