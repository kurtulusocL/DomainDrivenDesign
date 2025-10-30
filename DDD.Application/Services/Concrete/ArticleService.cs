using System.Linq.Expressions;
using AutoMapper;
using DDD.Application.Dtos.MappingDtos.ArticleMappingDto;
using DDD.Application.Services.Abstract;
using DDD.Domain.Entities;
using DDD.Domain.Repositories.Abstract;
using DDD.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;

namespace DDD.Application.Services.Concrete
{
    public class ArticleService : IArticleService
    {
        readonly IArticleRepository _articleRepository;
        readonly IMapper _mapper;
        public ArticleService(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(string title, string subtitle, string? detail, string description, int categoryId, int writerId, IFormFile image)
        {
            try
            {
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/article/");
                    if (!Directory.Exists(directoryPath))
                    {
                        Console.WriteLine($"Path is preparing: {directoryPath}");
                        Directory.CreateDirectory(directoryPath);
                    }
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine(directoryPath, fileName);
                    try
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }
                        var entity = new ArticleCreateDto
                        {
                            Title = title,
                            Subtitle = subtitle,
                            Detail = detail,
                            Description = description,
                            CategoryId = categoryId,
                            WriterId = writerId
                        };
                        if (entity != null)
                        {
                            entity.ImageUrl = fileName;
                            var result = _mapper.Map<Article>(entity);
                            return await _articleRepository.AddAsync(result);
                        }
                        return false;
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Error {fileName} : {ex.Message}");
                    }
                }
                throw new Exception("No Image for upload.");
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
                var data = await _articleRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _articleRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<ArticleDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _articleRepository.GetAllIncludeAsync(new Expression<Func<Article, bool>>[]
                {
                    i=>i.IsDeleted==false
                }, null, y => y.Writer, y => y.Category);
                return _mapper.Map<IEnumerable<ArticleDto>>(data.OrderByDescending(i => i.CreatedDate).ToList());
            }
            catch (Exception)
            {
                return new List<ArticleDto>();
            }
        }

        public async Task<IEnumerable<ArticleDto>> GetAllIncludingByCategoryIdAsync(int categoryId)
        {
            try
            {
                var data = await _articleRepository.GetAllIncludeByIdAsync(categoryId, "CategoryId", new Expression<Func<Article, bool>>[]
                {
                    i=>i.IsDeleted==false
                }, null, y => y.Writer, y => y.Category);
                return _mapper.Map<IEnumerable<ArticleDto>>(data.OrderByDescending(i => i.CreatedDate).ToList());
            }
            catch (Exception)
            {
                return new List<ArticleDto>();
            }
        }

        public async Task<IEnumerable<ArticleDto>> GetAllIncludingByWriterIdAsync(int writerId)
        {
            try
            {
                var data = await _articleRepository.GetAllIncludeByIdAsync(writerId, "WriterId", new Expression<Func<Article, bool>>[]
                {
                    i=>i.IsDeleted==false
                }, null, y => y.Writer, y => y.Category);
                return _mapper.Map<IEnumerable<ArticleDto>>(data.OrderByDescending(i => i.CreatedDate).ToList());
            }
            catch (Exception)
            {
                return new List<ArticleDto>();
            }
        }

        public async Task<IEnumerable<ArticleDto>> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = await _articleRepository.GetAllIncludeAsync(new Expression<Func<Article, bool>>[]
                {

                }, null, y => y.Writer, y => y.Category);
                return _mapper.Map<IEnumerable<ArticleDto>>(data.OrderByDescending(i => i.CreatedDate).ToList());
            }
            catch (Exception)
            {
                return new List<ArticleDto>();
            }
        }

        public async Task<ArticleDto> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                var data = await _articleRepository.GetIncludeAsync(i => i.Id == id, y => y.Writer, y => y.Category);
                return _mapper.Map<ArticleDto>(data);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<ArticleDto> SetDeletedAsync(int id)
        {
            var result = await _articleRepository.SetDeletedAsync(i => i.Id == id);
            return _mapper.Map<ArticleDto>(result);
        }

        public async Task<ArticleDto> SetNotDeletedAsync(int id)
        {
            var result = await _articleRepository.SetNotDeletedAsync(i => i.Id == id);
            return _mapper.Map<ArticleDto>(result);
        }

        public async Task<bool> UpdateAsync(string title, string subtitle, string? detail, string description, int categoryId, int writerId, IFormFile image, int id)
        {
            try
            {
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/article/");
                    if (!Directory.Exists(directoryPath))
                    {
                        Console.WriteLine($"Path is preparing: {directoryPath}");
                        Directory.CreateDirectory(directoryPath);
                    }
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine(directoryPath, fileName);
                    try
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }
                        var entity = new ArticleUpdateDto
                        {
                            Title = title,
                            Subtitle = subtitle,
                            Detail = detail,
                            Description = description,
                            CategoryId = categoryId,
                            WriterId = writerId,
                            Id = id
                        };
                        if (entity != null)
                        {
                            entity.ImageUrl = fileName;
                            var result = _mapper.Map<Article>(entity);
                            return await _articleRepository.UpdateAsync(result);
                        }
                        return false;
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Error {fileName} : {ex.Message}");
                    }
                }
                throw new Exception("No Image for upload.");
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
