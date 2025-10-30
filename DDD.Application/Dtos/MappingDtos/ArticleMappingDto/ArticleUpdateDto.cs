
namespace DDD.Application.Dtos.MappingDtos.ArticleMappingDto
{
    public class ArticleUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string? Detail { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int WriterId { get; set; }
    }
}
