using DDD.Application.Dtos.MappingDtos.UserSessionMappingDto;

namespace DDD.Application.Services.Abstract
{
    public interface IUserSessionService
    {
        Task<IEnumerable<UserSessionDto>> GetAllIncludingAsync();
        Task<IEnumerable<UserSessionDto>> GetAllIncludingByOnlineUserAsync();
        Task<IEnumerable<UserSessionDto>> GetAllIncludingByOfflineUserAsync();
        Task<IEnumerable<UserSessionDto>> GetAllIncludingByUserIdAsync(string userId);
        Task<IEnumerable<UserSessionDto>> GetAllIncludingForAdminAsync();
        Task<UserSessionDto> GetByIdAsync(int? id);
        Task<bool> DeleteAsync(int id);
        Task<UserSessionDto> SetDeletedAsync(int id);
        Task<UserSessionDto> SetNotDeletedAsync(int id);
    }
}
