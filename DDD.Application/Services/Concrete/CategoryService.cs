using System.Linq.Expressions;
using AutoMapper;
using DDD.Application.Dtos.MappingDtos.CategoryMappingDtos;
using DDD.Application.Services.Abstract;
using DDD.Domain.Entities;
using DDD.Domain.Repositories.Abstract;

namespace DDD.Application.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        readonly ICategoryRepository _categoryRepository;
        readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(CategoryCreateDto entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = _mapper.Map<Category>(entity);
                return await _categoryRepository.AddAsync(result);
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
                var category = await _categoryRepository.GetAsync(x => x.Id == id);
                if (category == null) 
                    return false;

                return await _categoryRepository.DeleteAsync(category);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _categoryRepository.GetAllIncludeAsync(new Expression<Func<Category, bool>>[]
                {
                    i=>i.IsDeleted==false
                }, null, y => y.Articles);
                return _mapper.Map<IEnumerable<CategoryDto>>(data.OrderByDescending(i => i.CreatedDate));
            }
            catch (Exception)
            {
                return new List<CategoryDto>();
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = await _categoryRepository.GetAllIncludeAsync(new Expression<Func<Category, bool>>[]
                {

                }, null, y => y.Articles);
                return _mapper.Map<IEnumerable<CategoryDto>>(data.OrderByDescending(i => i.CreatedDate));
            }
            catch (Exception)
            {
                return new List<CategoryDto>();
            }
        }

        public async Task<CategoryDto> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                var data = await _categoryRepository.GetIncludeAsync(i => i.Id == id, y => y.Articles);
                return _mapper.Map<CategoryDto>(data);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<CategoryDto> SetDeletedAsync(int id)
        {
            var result = await _categoryRepository.SetDeletedAsync(i => i.Id == id);
            return _mapper.Map<CategoryDto>(result);
        }

        public async Task<CategoryDto> SetNotDeletedAsync(int id)
        {
            var result = await _categoryRepository.SetNotDeletedAsync(i => i.Id == id);
            return _mapper.Map<CategoryDto>(result);
        }

        public async Task<bool> UpdateAsync(CategoryUpdateDto entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = _mapper.Map<Category>(entity);
                return await _categoryRepository.UpdateAsync(result);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
