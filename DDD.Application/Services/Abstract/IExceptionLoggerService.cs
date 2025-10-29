using DDD.Domain.Entities;

namespace DDD.Application.Services.Abstract
{
    public interface IExceptionLoggerService
    {
        Task<IEnumerable<ExceptionLogger>> GetAllAsync();
        Task<IEnumerable<ExceptionLogger>> GetAllForAdminAsync();
        Task<ExceptionLogger> GetByIdAsync(int? id);
        Task<bool> CreateAsync(ExceptionLogger entity);
        Task<bool> DeleteAsync(ExceptionLogger entity, int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
