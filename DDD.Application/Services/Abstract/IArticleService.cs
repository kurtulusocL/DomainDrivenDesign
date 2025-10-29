using DDD.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace DDD.Application.Services.Abstract
{
    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetAllIncludingAsync();
        Task<IEnumerable<Article>> GetAllIncludingByWriterIdAsync(int writerId);
        Task<IEnumerable<Article>> GetAllIncludingByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Article>> GetAllIncludingForAdminAsync();
        Task<Article> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string title, string subtitle, string? detail, string description, int categoryId, int writerId, IFormFile image);
        Task<bool> UpdateAsync(string title, string subtitle, string? detail, string description, int categoryId, int writerId, IFormFile image, int id);
        Task<bool> DeleteAsync(Article entity, int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
