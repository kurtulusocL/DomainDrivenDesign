using DDD.Application.Dtos.MappingDtos.ArticleMappingDto;
using DDD.Application.Dtos.MappingDtos.ArticleMappingDto.Requests;
using DDD.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace DDD.Application.Services.Abstract
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleDto>> GetAllIncludingAsync();
        Task<IEnumerable<ArticleDto>> GetAllIncludingByWriterIdAsync(int writerId);
        Task<IEnumerable<ArticleDto>> GetAllIncludingByCategoryIdAsync(int categoryId);
        Task<IEnumerable<ArticleDto>> GetAllIncludingForAdminAsync();
        Task<ArticleDto> GetByIdAsync(int? id);
        Task<bool> CreateAsync(ArticleCreateRequest request);
        Task<bool> UpdateAsync(ArticleUpdateRequest request);
        //Task<bool> CreateAsync(string title, string subtitle, string? detail, string description, int categoryId, int writerId, IFormFile image);
        //Task<bool> UpdateAsync(string title, string subtitle, string? detail, string description, int categoryId, int writerId, IFormFile image, int id);
        Task<bool> DeleteAsync(int id);
        Task<ArticleDto> SetDeletedAsync(int id);
        Task<ArticleDto> SetNotDeletedAsync(int id);
    }
}
