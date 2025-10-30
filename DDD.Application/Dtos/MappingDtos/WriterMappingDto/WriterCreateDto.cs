using Microsoft.AspNetCore.Http;

namespace DDD.Application.Dtos.MappingDtos.WriterMappingDto
{
    public class WriterCreateDto
    {
        public string NameSurname { get; set; }
        public string Title { get; set; }
        public string? ImageUrl { get; set; }
    }
}
