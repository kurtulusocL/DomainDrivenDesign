using DDD.Application.Dtos.MappingDtos.CategoryMappingDtos;
using DDD.Domain.Entities;

namespace DDD.Application.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllIncludingAsync();
        Task<IEnumerable<CategoryDto>> GetAllIncludingForAdminAsync();
        Task<CategoryDto> GetByIdAsync(int? id);
        Task<bool> CreateAsync(CategoryCreateDto entity);
        Task<bool> UpdateAsync(CategoryUpdateDto entity);
        Task<bool> DeleteAsync(int id);
        Task<CategoryDto> SetDeletedAsync(int id);
        Task<CategoryDto> SetNotDeletedAsync(int id);
    }
}
