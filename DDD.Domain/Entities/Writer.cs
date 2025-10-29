using DDD.Domain.Entities.EntityFramework;

namespace DDD.Domain.Entities
{
    public class Writer : BaseEntity
    {
        public string NameSurname { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
