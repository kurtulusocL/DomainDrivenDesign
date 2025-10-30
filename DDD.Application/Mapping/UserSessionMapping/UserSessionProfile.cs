using AutoMapper;
using DDD.Application.Dtos.MappingDtos.UserSessionMappingDto;
using DDD.Domain.Entities;

namespace DDD.Application.Mapping.UserSessionMapping
{
    public class UserSessionProfile:Profile
    {
        public UserSessionProfile()
        {
            CreateMap<UserSession, UserSessionDto>();
        }
    }
}
