using AutoMapper;
using DDD.Application.Dtos.MappingDtos.UserMappingDto;
using DDD.Domain.Entities.EntityFramework.AppUser;

namespace DDD.Application.Mapping.UserMapping
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
