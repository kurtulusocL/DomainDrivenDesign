using System.Linq.Expressions;
using DDD.Application.Services.Abstract;
using DDD.Domain.Entities;
using DDD.Domain.Repositories.Abstract;

namespace DDD.Application.Services.Concrete
{
    public class UserSessionService : IUserSessionService
    {
        readonly IUserSessionRepository _userSessionRepository;
        public UserSessionService(IUserSessionRepository userSessionRepository)
        {
            _userSessionRepository = userSessionRepository;
        }

        public async Task<bool> DeleteAsync(UserSession entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entit was null");

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

        public async Task<IEnumerable<UserSession>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _userSessionRepository.GetAllIncludeAsync(new Expression<Func<UserSession, bool>>[]
                {
                    i=>i.IsDeleted==false
                }, null, y => y.User);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<UserSession>();
            }
        }

        public async Task<IEnumerable<UserSession>> GetAllIncludingByOfflineUserAsync()
        {
            try
            {
                var data = await _userSessionRepository.GetAllIncludeAsync(new Expression<Func<UserSession, bool>>[]
                {
                    i=>i.IsDeleted==false,
                    i=>i.IsOnline==false
                }, null, y => y.User);
                return data.OrderByDescending(i => i.LogoutDate).ToList();
            }
            catch (Exception)
            {
                return new List<UserSession>();
            }
        }

        public async Task<IEnumerable<UserSession>> GetAllIncludingByOnlineUserAsync()
        {
            try
            {
                var data = await _userSessionRepository.GetAllIncludeAsync(new Expression<Func<UserSession, bool>>[]
                {
                    i=>i.IsDeleted==false,
                    i=>i.IsOnline==true
                }, null, y => y.User);
                return data.OrderByDescending(i => i.LoginDate).ToList();
            }
            catch (Exception)
            {
                return new List<UserSession>();
            }
        }

        public async Task<IEnumerable<UserSession>> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = await _userSessionRepository.GetAllIncludeAsync(new Expression<Func<UserSession, bool>>[]
                {

                }, null, y => y.User);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<UserSession>();
            }
        }

        public async Task<IEnumerable<UserSession>> GetAllIncludingVyUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var data = await _userSessionRepository.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<UserSession, bool>>[]
                {
                    i=>i.IsDeleted==false
                }, null, y => y.User);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<UserSession>();
            }
        }

        public async Task<UserSession> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _userSessionRepository.GetIncludeAsync(i => i.Id == id, y => y.User);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _userSessionRepository.SetDeletedAsync(i => i.Id == id);
            return result != null;
        }

        public async Task<bool> SetNotDeleted(int id)
        {
            var result = await _userSessionRepository.SetNotDeletedAsync(i => i.Id == id);
            return result != null;
        }
    }
}