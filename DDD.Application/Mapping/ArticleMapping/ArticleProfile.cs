using AutoMapper;
using DDD.Application.Dtos.MappingDtos.ArticleMappingDto;
using DDD.Domain.Entities;

namespace DDD.Application.Mapping.ArticleMapping
{
    public class ArticleProfile:Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleDto>();
            CreateMap<ArticleCreateDto, Article>().ReverseMap();
            CreateMap<ArticleUpdateDto, Article>().ReverseMap();
        }
    }
}
