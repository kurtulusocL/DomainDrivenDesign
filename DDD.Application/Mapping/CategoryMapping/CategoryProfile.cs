using AutoMapper;
using DDD.Application.Dtos.MappingDtos.CategoryMappingDtos;
using DDD.Domain.Entities;

namespace DDD.Application.Mapping.CategoryMapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreateDto, Category>().ReverseMap();
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();
        }
    }
}
