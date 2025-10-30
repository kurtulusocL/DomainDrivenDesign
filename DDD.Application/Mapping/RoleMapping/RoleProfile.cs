using AutoMapper;
using DDD.Application.Dtos.MappingDtos.RoleMappingDto;
using DDD.Domain.Entities.EntityFramework.AppUser;

namespace DDD.Application.Mapping.RoleMapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<RoleCreateDto, Role>().ReverseMap();
            CreateMap<RoleUpdateDto, Role>().ReverseMap();
        }
    }
}
