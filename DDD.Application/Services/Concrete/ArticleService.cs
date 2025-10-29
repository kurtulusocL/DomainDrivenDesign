using System.Linq.Expressions;
using DDD.Application.Services.Abstract;
using DDD.Domain.Entities;
using DDD.Domain.Repositories.Abstract;
using Microsoft.AspNetCore.Http;

namespace DDD.Application.Services.Concrete
{
    public class ArticleService : IArticleService
    {
        readonly IArticleRepository _articleRepository;
        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
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
                        var entity = new Article
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
                            var result = await _articleRepository.AddAsync(entity);
                            if (!result)
                            {
                                errors.Add($"Error {fileName}.");
                            }
                            return true;
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

        public async Task<bool> DeleteAsync(Article entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entit was null");

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

        public async Task<IEnumerable<Article>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _articleRepository.GetAllIncludeAsync(new Expression<Func<Article, bool>>[]
                {
                    i=>i.IsDeleted==false
                }, null, y => y.Writer, y => y.Category);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Article>();
            }
        }

        public async Task<IEnumerable<Article>> GetAllIncludingByCategoryIdAsync(int categoryId)
        {
            try
            {
                var data = await _articleRepository.GetAllIncludeByIdAsync(categoryId, "CategoryId", new Expression<Func<Article, bool>>[]
                {
                    i=>i.IsDeleted==false
                }, null, y => y.Writer, y => y.Category);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Article>();
            }
        }

        public async Task<IEnumerable<Article>> GetAllIncludingByWriterIdAsync(int writerId)
        {
            try
            {
                var data = await _articleRepository.GetAllIncludeByIdAsync(writerId, "WriterId", new Expression<Func<Article, bool>>[]
                {
                    i=>i.IsDeleted==false
                }, null, y => y.Writer, y => y.Category);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Article>();
            }
        }

        public async Task<IEnumerable<Article>> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = await _articleRepository.GetAllIncludeAsync(new Expression<Func<Article, bool>>[]
                {

                }, null, y => y.Writer, y => y.Category);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Article>();
            }
        }

        public async Task<Article> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _articleRepository.GetIncludeAsync(i => i.Id == id, y => y.Writer, y => y.Category);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _articleRepository.SetDeletedAsync(i => i.Id == id);
            return result != null;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _articleRepository.SetNotDeletedAsync(i => i.Id == id);
            return result != null;
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
                        var entity = new Article
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
                            var result = await _articleRepository.UpdateAsync(entity);
                            if (!result)
                            {
                                errors.Add($"Error {fileName}.");
                            }
                            return true;
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
