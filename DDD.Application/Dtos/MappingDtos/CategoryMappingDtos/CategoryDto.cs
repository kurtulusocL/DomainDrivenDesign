
namespace DDD.Application.Dtos.MappingDtos.CategoryMappingDtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; } = DateTime.UtcNow;
        public DateTime CreatedDate { get; set; }
    }
}
