using System.Linq.Expressions;
using AutoMapper;
using DDD.Application.Dtos.MappingDtos.UserSessionMappingDto;
using DDD.Application.Services.Abstract;
using DDD.Domain.Entities;
using DDD.Domain.Repositories.Abstract;

namespace DDD.Application.Services.Concrete
{
    public class UserSessionService : IUserSessionService
    {
        readonly IUserSessionRepository _userSessionRepository;
        readonly IMapper _mapper;
        public UserSessionService(IUserSessionRepository userSessionRepository, IMapper mapper)
        {
            _userSessionRepository = userSessionRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var data = await _userSessionRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _userSessionRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<UserSessionDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _userSessionRepository.GetAllIncludeAsync(new Expression<Func<UserSession, bool>>[]
                {
                    i=>i.IsDeleted==false
                }, null, y => y.User);
                return _mapper.Map<IEnumerable<UserSessionDto>>(data.OrderByDescending(i => i.CreatedDate).ToList());
            }
            catch (Exception)
            {
                return new List<UserSessionDto>();
            }
        }

        public async Task<IEnumerable<UserSessionDto>> GetAllIncludingByOfflineUserAsync()
        {
            try
            {
                var data = await _userSessionRepository.GetAllIncludeAsync(new Expression<Func<UserSession, bool>>[]
                {
                    i=>i.IsDeleted==false,
                    i=>i.IsOnline==false &&i.LogoutDate!=null
                }, null, y => y.User);
                return _mapper.Map<IEnumerable<UserSessionDto>>(data.OrderByDescending(i => i.LogoutDate).ToList());
            }
            catch (Exception)
            {
                return new List<UserSessionDto>();
            }
        }

        public async Task<IEnumerable<UserSessionDto>> GetAllIncludingByOnlineUserAsync()
        {
            try
            {
                var data = await _userSessionRepository.GetAllIncludeAsync(new Expression<Func<UserSession, bool>>[]
                {
                    i=>i.IsDeleted==false,
                    i=>i.IsOnline==true
                }, null, y => y.User);
                return _mapper.Map<IEnumerable<UserSessionDto>>(data.OrderByDescending(i => i.LoginDate).ToList());
            }
            catch (Exception)
            {
                return new List<UserSessionDto>();
            }
        }

        public async Task<IEnumerable<UserSessionDto>> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = await _userSessionRepository.GetAllIncludeAsync(new Expression<Func<UserSession, bool>>[]
                {

                }, null, y => y.User);
                return _mapper.Map<IEnumerable<UserSessionDto>>(data.OrderByDescending(i => i.CreatedDate).ToList());
            }
            catch (Exception)
            {
                return new List<UserSessionDto>();
            }
        }

        public async Task<IEnumerable<UserSessionDto>> GetAllIncludingByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var data = await _userSessionRepository.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<UserSession, bool>>[]
                {
                    i=>i.IsDeleted==false
                }, null, y => y.User);
                return _mapper.Map<IEnumerable<UserSessionDto>>(data.OrderByDescending(i => i.CreatedDate).ToList());
            }
            catch (Exception)
            {
                return new List<UserSessionDto>();
            }
        }

        public async Task<UserSessionDto> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                var data = await _userSessionRepository.GetIncludeAsync(i => i.Id == id, y => y.User);
                return _mapper.Map<UserSessionDto>(data);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<UserSessionDto> SetDeletedAsync(int id)
        {
            var result = await _userSessionRepository.SetDeletedAsync(i => i.Id == id);
            return _mapper.Map<UserSessionDto>(result);
        }

        public async Task<UserSessionDto> SetNotDeletedAsync(int id)
        {
            var result = await _userSessionRepository.SetNotDeletedAsync(i => i.Id == id);
            return _mapper.Map<UserSessionDto>(result);
        }
    }
}