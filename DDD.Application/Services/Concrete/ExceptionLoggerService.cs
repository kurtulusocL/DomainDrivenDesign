using AutoMapper;
using DDD.Application.Dtos.MappingDtos.ExceptionLoggerMappingDto;
using DDD.Application.Services.Abstract;
using DDD.Domain.Entities;
using DDD.Domain.Repositories.Abstract;

namespace DDD.Application.Services.Concrete
{
    public class ExceptionLoggerService : IExceptionLoggerService
    {
        readonly IExceptionLoggerRepository _exceptionLoggerRepository;
        readonly IMapper _mapper;
        public ExceptionLoggerService(IExceptionLoggerRepository exceptionLoggerRepository, IMapper mapper)
        {
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(ExceptionLoggerCreateDto entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = _mapper.Map<ExceptionLogger>(entity);
                return await _exceptionLoggerRepository.AddAsync(result);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
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

        public async Task<IEnumerable<ExceptionLoggerDto>> GetAllAsync()
        {
            try
            {
                var data = await _exceptionLoggerRepository.GetAllAsync(i => i.IsDeleted == false);
                return _mapper.Map<IEnumerable<ExceptionLoggerDto>>(data.OrderByDescending(i => i.CreatedDate));
            }
            catch (Exception)
            {
                return new List<ExceptionLoggerDto>();
            }
        }

        public async Task<IEnumerable<ExceptionLoggerDto>> GetAllForAdminAsync()
        {
            try
            {
                var data = await _exceptionLoggerRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<ExceptionLoggerDto>>(data.OrderByDescending(i => i.CreatedDate));
            }
            catch (Exception)
            {
                return new List<ExceptionLoggerDto>();
            }
        }

        public async Task<ExceptionLoggerDto> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                var data = await _exceptionLoggerRepository.GetAsync(i => i.Id == id);
                return _mapper.Map<ExceptionLoggerDto>(data);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<ExceptionLoggerDto> SetDeletedAsync(int id)
        {
            var result = await _exceptionLoggerRepository.SetDeletedAsync(i => i.Id == id);
            return _mapper.Map<ExceptionLoggerDto>(result);
        }

        public async Task<ExceptionLoggerDto> SetNotDeletedAsync(int id)
        {
            var result = await _exceptionLoggerRepository.SetNotDeletedAsync(i => i.Id == id);
            return _mapper.Map<ExceptionLoggerDto>(result);
        }
    }
}
