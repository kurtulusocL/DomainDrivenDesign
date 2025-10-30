using System.Linq.Expressions;
using AutoMapper;
using DDD.Application.Dtos.MappingDtos.WriterMappingDto;
using DDD.Application.Dtos.MappingDtos.WriterMappingDto.Requests;
using DDD.Application.Services.Abstract;
using DDD.Domain.Entities;
using DDD.Domain.Repositories.Abstract;

namespace DDD.Application.Services.Concrete
{
    public class WriterService : IWriterService
    {
        readonly IWriterRepository _writerRepository;
        readonly IMapper _mapper;
        public WriterService(IWriterRepository writerRepository, IMapper mapper)
        {
            _writerRepository = writerRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(WriterCreateRequest request)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request), "request was null");

                string imageUrl = null;

                if (request.Image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/writer/");
                    if (!Directory.Exists(directoryPath))
                    {
                        Console.WriteLine($"Path is preparing: {directoryPath}");
                        Directory.CreateDirectory(directoryPath);
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Image.FileName);
                    var filePath = Path.Combine(directoryPath, fileName);

                    try
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await request.Image.CopyToAsync(stream);
                        }

                        imageUrl = $"/img/writer/{fileName}";
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Error uploading image: {ex.Message}", ex);
                    }
                }
                var writer = new Writer
                {
                    NameSurname = request.NameSurname,
                    Title = request.Title,
                    ImageUrl = imageUrl
                };
                var result = _mapper.Map<Writer>(request);
                return await _writerRepository.AddAsync(result);
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

        public async Task<IEnumerable<WriterDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _writerRepository.GetAllIncludeAsync(new Expression<Func<Writer, bool>>[]
                {
                    i=>i.IsDeleted==false
                }, null, y => y.Articles);
                return _mapper.Map<IEnumerable<WriterDto>>(data.OrderByDescending(i => i.CreatedDate).ToList());
            }
            catch (Exception)
            {
                return new List<WriterDto>();
            }
        }

        public async Task<IEnumerable<WriterDto>> GetAllIncludingForAddWriterAsync()
        {
            try
            {
                var data = await _writerRepository.GetAllIncludeAsync(new Expression<Func<Writer, bool>>[]
                {
                    i=>i.IsDeleted==false
                }, null, y => y.Articles);
                return _mapper.Map<IEnumerable<WriterDto>>(data.OrderByDescending(i => i.Articles.Count()).ToList());
            }
            catch (Exception)
            {
                return new List<WriterDto>();
            }
        }

        public async Task<IEnumerable<WriterDto>> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = await _writerRepository.GetAllIncludeAsync(new Expression<Func<Writer, bool>>[]
                {

                }, null, y => y.Articles);
                return _mapper.Map<IEnumerable<WriterDto>>(data.OrderByDescending(i => i.CreatedDate).ToList());
            }
            catch (Exception)
            {
                return new List<WriterDto>();
            }
        }

        public async Task<WriterDto> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                var data = await _writerRepository.GetIncludeAsync(i => i.Id == id, y => y.Articles);
                return _mapper.Map<WriterDto>(data);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<WriterDto> SetDeletedAsync(int id)
        {
            var result = await _writerRepository.SetDeletedAsync(i => i.Id == id);
            return _mapper.Map<WriterDto>(result);
        }

        public async Task<WriterDto> SetNotDeletedAsync(int id)
        {
            var result = await _writerRepository.SetNotDeletedAsync(i => i.Id == id);
            return _mapper.Map<WriterDto>(result);
        }

        public async Task<bool> UpdateAsync(WriterUpdateRequest request)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request), "request was null");

                string imageUrl = null;

                if (request.CurrentImageUrl != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/writer/");
                    if (!Directory.Exists(directoryPath))
                    {
                        Console.WriteLine($"Path is preparing: {directoryPath}");
                        Directory.CreateDirectory(directoryPath);
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.ImageUrl.FileName);
                    var filePath = Path.Combine(directoryPath, fileName);

                    try
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await request.ImageUrl.CopyToAsync(stream);
                        }

                        imageUrl = $"/img/writer/{fileName}";
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Error uploading image: {ex.Message}", ex);
                    }
                }
                var writer = new Writer
                {
                    NameSurname = request.NameSurname,
                    Title = request.Title,
                    ImageUrl = imageUrl
                };

                var result = _mapper.Map<Writer>(request);
                return await _writerRepository.UpdateAsync(result);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}