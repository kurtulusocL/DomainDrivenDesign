using DDD.Application.Services.Abstract;
using DDD.Domain.Entities.EntityFramework.AppUser;
using DDD.Domain.Repositories.Abstract;

namespace DDD.Application.Services.Concrete
{
    public class RoleService : IRoleService
    {
        readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> CreateAsync(Role entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _roleRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Role entity, string id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entit was null");

                var data = await _roleRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _roleRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            try
            {
                var data = await _roleRepository.GetAllAsync(i => i.IsDeleted == false);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Role>();
            }
        }

        public async Task<IEnumerable<Role>> GetAllForAdminAsync()
        {
            try
            {
                var data = await _roleRepository.GetAllAsync();
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Role>();
            }
        }

        public async Task<Role> GetByIdAsync(string? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _roleRepository.GetAsync(i => i.Id == id);
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

                var result = await _roleRepository.SetDeletedAsync(i => i.Id == id);
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

                var result = await _roleRepository.SetNotDeletedAsync(i => i.Id == id);
                return result != null;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Not Deleted the entity.", ex);
            }
        }

        public async Task<bool> UpdateAsync(Role entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _roleRepository.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
