using System.Linq.Expressions;
using DDD.Application.Services.Abstract;
using DDD.Domain.Entities;
using DDD.Domain.Repositories.Abstract;
using Microsoft.AspNetCore.Http;

namespace DDD.Application.Services.Concrete
{
    public class WriterService : IWriterService
    {
        readonly IWriterRepository _writerRepository;
        public WriterService(IWriterRepository writerRepository)
        {
            _writerRepository = writerRepository;
        }

        public async Task<bool> CreateAsync(Writer entity, IFormFile image)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/writer/");
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
                        entity.ImageUrl = fileName;
                        var result = await _writerRepository.AddAsync(entity);
                        if (!result)
                        {
                            errors.Add($"Error {fileName}.");
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Error {fileName} : {ex.Message}");
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Writer entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entit was null");

                var data = await _writerRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _writerRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Writer>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _writerRepository.GetAllIncludeAsync(new Expression<Func<Writer, bool>>[]
                {
                    i=>i.IsDeleted==false
                }, null, y => y.Articles);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Writer>();
            }
        }

        public async Task<IEnumerable<Writer>> GetAllIncludingForAddWriterAsync()
        {
            try
            {
                var data = await _writerRepository.GetAllIncludeAsync(new Expression<Func<Writer, bool>>[]
                {
                    i=>i.IsDeleted==false
                }, null, y => y.Articles);
                return data.OrderByDescending(i => i.Articles.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Writer>();
            }
        }

        public async Task<IEnumerable<Writer>> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = await _writerRepository.GetAllIncludeAsync(new Expression<Func<Writer, bool>>[]
                {

                }, null, y => y.Articles);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Writer>();
            }
        }

        public async Task<Writer> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _writerRepository.GetIncludeAsync(i => i.Id == id, y => y.Articles);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _writerRepository.SetDeletedAsync(i => i.Id == id);
            return result != null;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _writerRepository.SetNotDeletedAsync(i => i.Id == id);
            return result != null;
        }

        public async Task<bool> UpdateAsync(Writer entity, IFormFile image)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/writer/");
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
                        entity.ImageUrl = fileName;
                        var result = await _writerRepository.UpdateAsync(entity);
                        if (!result)
                        {
                            errors.Add($"Error {fileName}.");
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Error {fileName} : {ex.Message}");
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
