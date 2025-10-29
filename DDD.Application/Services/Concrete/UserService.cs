using System.Linq.Expressions;
using DDD.Application.Services.Abstract;
using DDD.Domain.Entities.EntityFramework.AppUser;
using DDD.Domain.Repositories.Abstract;

namespace DDD.Application.Services.Concrete
{
    public class UserService : IUserService
    {
        readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> DeleteAsync(User entity, string id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entit was null");

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

        public async Task<IEnumerable<User>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _userRepository.GetAllIncludeAsync(new Expression<Func<User, bool>>[]
                {
                    i=>i.IsDeleted==false
                }, null, y => y.UserSessions);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }

        public async Task<IEnumerable<User>> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = await _userRepository.GetAllIncludeAsync(new Expression<Func<User, bool>>[]
                {

                }, null, y => y.UserSessions);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }

        public async Task<User> GetByIdAsync(string? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _userRepository.GetIncludeAsync(i => i.Id == id, y => y.UserSessions);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetDeletedAsync(string id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                var result = await _userRepository.SetDeletedAsync(i => i.Id == id);
                return result != null;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetNotDeletedAsync(string id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                var result = await _userRepository.SetNotDeletedAsync(i => i.Id == id);
                return result != null;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Not Deleted the entity.", ex);
            }
        }
    }
}
