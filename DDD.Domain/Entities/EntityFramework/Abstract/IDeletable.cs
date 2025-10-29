
namespace DDD.Domain.Entities.EntityFramework.Abstract
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedDate { get; set; }
    }
}
