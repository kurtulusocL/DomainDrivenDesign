using DDD.Application.Dtos.MappingDtos.WriterMappingDto;
using DDD.Application.Dtos.MappingDtos.WriterMappingDto.Requests;
using Microsoft.AspNetCore.Http;

namespace DDD.Application.Services.Abstract
{
    public interface IWriterService
    {
        Task<IEnumerable<WriterDto>> GetAllIncludingAsync();
        Task<IEnumerable<WriterDto>> GetAllIncludingForAddWriterAsync();
        Task<IEnumerable<WriterDto>> GetAllIncludingForAdminAsync();
        Task<WriterDto> GetByIdAsync(int? id);
        Task<bool> CreateAsync(WriterCreateRequest request);
        Task<bool> UpdateAsync(WriterUpdateRequest request);
        Task<bool> DeleteAsync(int id);
        Task<WriterDto> SetDeletedAsync(int id);
        Task<WriterDto> SetNotDeletedAsync(int id);
    }
}
