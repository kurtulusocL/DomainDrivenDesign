using DDD.Application.Dtos.MappingDtos.UserMappingDto;
using DDD.Domain.Entities.EntityFramework.AppUser;

namespace DDD.Application.Services.Abstract
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllIncludingAsync();
        Task<IEnumerable<UserDto>> GetAllIncludingForAdminAsync();
        Task<UserDto> GetByIdAsync(string? id);
        Task<bool> DeleteAsync(string id);
        Task<UserDto> SetDeletedAsync(string id);
        Task<UserDto> SetNotDeletedAsync(string id);
    }
}
