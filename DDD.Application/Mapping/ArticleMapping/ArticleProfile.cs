using AutoMapper;
using DDD.Application.Dtos.MappingDtos.ArticleMappingDto;
using DDD.Application.Dtos.MappingDtos.ArticleMappingDto.Requests;
using DDD.Domain.Entities;

namespace DDD.Application.Mapping.ArticleMapping
{
    public class ArticleProfile:Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleDto>();
            CreateMap<ArticleCreateRequest, Article>().ReverseMap();
            CreateMap<ArticleCreateDto, Article>().ReverseMap();
            CreateMap<ArticleUpdateDto, Article>().ReverseMap();
            CreateMap<ArticleUpdateRequest, Article>().ReverseMap();
        }
    }
}
