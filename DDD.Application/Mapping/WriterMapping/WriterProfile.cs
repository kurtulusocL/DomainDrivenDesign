using AutoMapper;
using DDD.Application.Dtos.MappingDtos.WriterMappingDto;
using DDD.Domain.Entities;

namespace DDD.Application.Mapping.WriterMapping
{
    public class WriterProfile:Profile
    {
        public WriterProfile()
        {
            CreateMap<Writer, WriterDto>();
            CreateMap<WriterCreateDto, Writer>().ReverseMap();
            CreateMap<WriterUpdateDto, Writer>().ReverseMap();
        }
    }
}
