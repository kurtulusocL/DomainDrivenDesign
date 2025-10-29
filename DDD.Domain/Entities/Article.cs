using DDD.Domain.Entities.EntityFramework;

namespace DDD.Domain.Entities
{
    public class Article : BaseEntity
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string? Detail { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public int WriterId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Writer Writer { get; set; }
    }
}
