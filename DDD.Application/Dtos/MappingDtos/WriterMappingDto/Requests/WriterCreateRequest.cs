using Microsoft.AspNetCore.Http;

namespace DDD.Application.Dtos.MappingDtos.WriterMappingDto.Requests
{
    public class WriterCreateRequest
    {
        public string NameSurname { get; set; }
        public string Title { get; set; }
        public IFormFile Image { get; set; }
    }
}
