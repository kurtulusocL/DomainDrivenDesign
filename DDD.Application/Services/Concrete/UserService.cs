using System.Linq.Expressions;
using AutoMapper;
using DDD.Application.Dtos.MappingDtos.UserMappingDto;
using DDD.Application.Services.Abstract;
using DDD.Domain.Entities.EntityFramework.AppUser;
using DDD.Domain.Repositories.Abstract;

namespace DDD.Application.Services.Concrete
{
    public class UserService : IUserService
    {
        readonly IUserRepository _userRepository;
        readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var data = await _userRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _userRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _userRepository.GetAllIncludeAsync(new Expression<Func<User, bool>>[]
                {
                    i=>i.IsDeleted==false
                }, null, y => y.UserSessions);
                return _mapper.Map<IEnumerable<UserDto>>(data.OrderByDescending(i => i.CreatedDate).ToList());
            }
            catch (Exception)
            {
                return new List<UserDto>();
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = await _userRepository.GetAllIncludeAsync(new Expression<Func<User, bool>>[]
                {

                }, null, y => y.UserSessions);
                return _mapper.Map<IEnumerable<UserDto>>(data.OrderByDescending(i => i.CreatedDate).ToList());
            }
            catch (Exception)
            {
                return new List<UserDto>();
            }
        }

        public async Task<UserDto> GetByIdAsync(string id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                var data = await _userRepository.GetIncludeAsync(i => i.Id == id, y => y.UserSessions);
                return _mapper.Map<UserDto>(data);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<UserDto> SetDeletedAsync(string id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                var result = await _userRepository.SetDeletedAsync(i => i.Id == id);
                return _mapper.Map<UserDto>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<UserDto> SetNotDeletedAsync(string id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                var result = await _userRepository.SetNotDeletedAsync(i => i.Id == id);
                return _mapper.Map<UserDto>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Not Deleted the entity.", ex);
            }
        }
    }
}
