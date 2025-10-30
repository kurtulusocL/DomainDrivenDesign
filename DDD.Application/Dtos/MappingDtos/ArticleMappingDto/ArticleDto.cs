
namespace DDD.Application.Dtos.MappingDtos.ArticleMappingDto
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string? Detail { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; } = DateTime.UtcNow;
        public DateTime CreatedDate { get; set; }

        public int CategoryId { get; set; }
        public int WriterId { get; set; }

    }
}
