using DDD.Application.Services.Abstract;
using DDD.Domain.Entities;
using DDD.Domain.Repositories.Abstract;

namespace DDD.Application.Services.Concrete
{
    public class ExceptionLoggerService : IExceptionLoggerService
    {
        readonly IExceptionLoggerRepository _exceptionLoggerRepository;
        public ExceptionLoggerService(IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _exceptionLoggerRepository = exceptionLoggerRepository;
        }

        public async Task<bool> CreateAsync(ExceptionLogger entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _exceptionLoggerRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(ExceptionLogger entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entit was null");

                var data = await _exceptionLoggerRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _exceptionLoggerRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<ExceptionLogger>> GetAllAsync()
        {
            try
            {
                var data = await _exceptionLoggerRepository.GetAllAsync(i => i.IsDeleted == false);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ExceptionLogger>();
            }
        }

        public async Task<IEnumerable<ExceptionLogger>> GetAllForAdminAsync()
        {
            try
            {
                var data = await _exceptionLoggerRepository.GetAllAsync();
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ExceptionLogger>();
            }
        }

        public async Task<ExceptionLogger> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _exceptionLoggerRepository.GetAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _exceptionLoggerRepository.SetDeletedAsync(i => i.Id == id);
            return result != null;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _exceptionLoggerRepository.SetNotDeletedAsync(i => i.Id == id);
            return result != null;
        }
    }
}
