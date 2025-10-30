
namespace DDD.Application.Dtos.MappingDtos.WriterMappingDto
{
    public class WriterDto
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; } = DateTime.UtcNow;
        public DateTime CreatedDate { get; set; }
    }
}
