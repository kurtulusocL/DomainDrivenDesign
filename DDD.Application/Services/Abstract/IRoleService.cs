using DDD.Domain.Entities.EntityFramework.AppUser;

namespace DDD.Application.Services.Abstract
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllAsync();
        Task<IEnumerable<Role>> GetAllForAdminAsync();
        Task<Role> GetByIdAsync(string? id);
        Task<bool> CreateAsync(Role entity);
        Task<bool> UpdateAsync(Role entity);
        Task<bool> DeleteAsync(Role entity, string id);
        Task<bool> SetDeletedAsync(string id);
        Task<bool> SetNotDeletedAsync(string id);
    }
}
