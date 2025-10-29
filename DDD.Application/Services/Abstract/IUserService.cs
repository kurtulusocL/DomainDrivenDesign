using DDD.Domain.Entities.EntityFramework.AppUser;

namespace DDD.Application.Services.Abstract
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllIncludingAsync();
        Task<IEnumerable<User>> GetAllIncludingForAdminAsync();
        Task<User> GetByIdAsync(string? id);
        Task<bool> DeleteAsync(User entity, string id);
        Task<bool> SetDeletedAsync(string id);
        Task<bool> SetNotDeletedAsync(string id);
    }
}
