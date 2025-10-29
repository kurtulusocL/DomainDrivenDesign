using DDD.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace DDD.Application.Services.Abstract
{
    public interface IWriterService
    {
        Task<IEnumerable<Writer>> GetAllIncludingAsync();
        Task<IEnumerable<Writer>> GetAllIncludingForAddWriterAsync();
        Task<IEnumerable<Writer>> GetAllIncludingForAdminAsync();
        Task<Writer> GetByIdAsync(int? id);
        Task<bool> CreateAsync(Writer entity, IFormFile image);
        Task<bool>UpdateAsync(Writer entity, IFormFile image);
        Task<bool> DeleteAsync(Writer entity, int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
