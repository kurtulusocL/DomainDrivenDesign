using DDD.Domain.Entities.EntityFramework;

namespace DDD.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
