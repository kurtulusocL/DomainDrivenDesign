using AutoMapper;
using DDD.Application.Dtos.MappingDtos.ExceptionLoggerMappingDto;
using DDD.Domain.Entities;

namespace DDD.Application.Mapping.ExceptionLoggerMapping
{
    public class ExceptionLoggerProfile : Profile
    {
        public ExceptionLoggerProfile()
        {
            CreateMap<ExceptionLogger, ExceptionLoggerDto>();
            CreateMap<ExceptionLoggerCreateDto, ExceptionLogger>().ReverseMap();
        }
    }
}
