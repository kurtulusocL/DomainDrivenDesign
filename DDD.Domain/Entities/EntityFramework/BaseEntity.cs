using System.ComponentModel.DataAnnotations;
using DDD.Domain.Entities.EntityFramework.Abstract;

namespace DDD.Domain.Entities.EntityFramework
{
    public class BaseEntity : IEntity, IDeletable
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }

        public void SetCreatedDate()
        {
            CreatedDate = DateTime.UtcNow;
        }

        public void SetDeleted()
        {
            IsDeleted = false;
        }
        public BaseEntity()
        {
            SetDeleted();
            SetCreatedDate();
        }
    }
}
