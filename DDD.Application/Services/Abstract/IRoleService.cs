using DDD.Application.Dtos.MappingDtos.RoleMappingDto;

namespace DDD.Application.Services.Abstract
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllAsync();
        Task<IEnumerable<RoleDto>> GetAllForAdminAsync();
        Task<RoleDto> GetByIdAsync(string? id);
        Task<bool> CreateAsync(RoleCreateDto entity);
        Task<bool> UpdateAsync(RoleUpdateDto entity);
        Task<bool> DeleteAsync(string id);
        Task<RoleDto> SetDeletedAsync(string id);
        Task<RoleDto> SetNotDeletedAsync(string id);
    }
}
