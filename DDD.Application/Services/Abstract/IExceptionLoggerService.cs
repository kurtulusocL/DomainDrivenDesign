using DDD.Application.Dtos.MappingDtos.ExceptionLoggerMappingDto;

namespace DDD.Application.Services.Abstract
{
    public interface IExceptionLoggerService
    {
        Task<IEnumerable<ExceptionLoggerDto>> GetAllAsync();
        Task<IEnumerable<ExceptionLoggerDto>> GetAllForAdminAsync();
        Task<ExceptionLoggerDto> GetByIdAsync(int? id);
        Task<bool> CreateAsync(ExceptionLoggerCreateDto entity);
        Task<bool> DeleteAsync(int id);
        Task<ExceptionLoggerDto> SetDeletedAsync(int id);
        Task<ExceptionLoggerDto> SetNotDeletedAsync(int id);
    }
}
