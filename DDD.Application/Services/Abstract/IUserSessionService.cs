using DDD.Domain.Entities;

namespace DDD.Application.Services.Abstract
{
    public interface IUserSessionService
    {
        Task<IEnumerable<UserSession>> GetAllIncludingAsync();
        Task<IEnumerable<UserSession>> GetAllIncludingByOnlineUserAsync();
        Task<IEnumerable<UserSession>> GetAllIncludingByOfflineUserAsync();
        Task<IEnumerable<UserSession>> GetAllIncludingVyUserIdAsync(string userId);
        Task<IEnumerable<UserSession>> GetAllIncludingForAdminAsync();
        Task<UserSession> GetByIdAsync(int? id);
        Task<bool> DeleteAsync(UserSession entity, int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeleted(int id);
    }
}
