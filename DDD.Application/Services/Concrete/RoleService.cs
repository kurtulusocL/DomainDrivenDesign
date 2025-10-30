using AutoMapper;
using DDD.Application.Dtos.MappingDtos.RoleMappingDto;
using DDD.Application.Services.Abstract;
using DDD.Domain.Entities.EntityFramework.AppUser;
using DDD.Domain.Repositories.Abstract;

namespace DDD.Application.Services.Concrete
{
    public class RoleService : IRoleService
    {
        readonly IRoleRepository _roleRepository;
        readonly IMapper _mapper;
        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(RoleCreateDto entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = _mapper.Map<Role>(entity);
                return await _roleRepository.AddAsync(result);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
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

        public async Task<IEnumerable<RoleDto>> GetAllAsync()
        {
            try
            {
                var data = await _roleRepository.GetAllAsync(i => i.IsDeleted == false);
                return _mapper.Map<IEnumerable<RoleDto>>(data.OrderByDescending(i => i.CreatedDate).ToList());
            }
            catch (Exception)
            {
                return new List<RoleDto>();
            }
        }

        public async Task<IEnumerable<RoleDto>> GetAllForAdminAsync()
        {
            try
            {
                var data = await _roleRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<RoleDto>>(data.OrderByDescending(i => i.CreatedDate).ToList());
            }
            catch (Exception)
            {
                return new List<RoleDto>();
            }
        }

        public async Task<RoleDto> GetByIdAsync(string? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                var data = await _roleRepository.GetAsync(i => i.Id == id);
                return _mapper.Map<RoleDto>(data);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<RoleDto> SetDeletedAsync(string id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                var result = await _roleRepository.SetDeletedAsync(i => i.Id == id);
                return _mapper.Map<RoleDto>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<RoleDto> SetNotDeletedAsync(string id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                var result = await _roleRepository.SetNotDeletedAsync(i => i.Id == id);
                return _mapper.Map<RoleDto>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Not Deleted the entity.", ex);
            }
        }

        public async Task<bool> UpdateAsync(RoleUpdateDto entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = _mapper.Map<Role>(entity);
                return await _roleRepository.UpdateAsync(result);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
