using Microsoft.AspNetCore.Http;

namespace DDD.Application.Dtos.MappingDtos.WriterMappingDto.Requests
{
    public class WriterUpdateRequest
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string Title { get; set; }
        public IFormFile ImageUrl { get; set; }
        public string CurrentImageUrl { get; set; }
    }
}
