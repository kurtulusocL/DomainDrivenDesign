

namespace DDD.Domain.Entities.EntityFramework.Abstract
{
    public interface IEntity
    {
        void SetCreatedDate();
        void SetDeleted();
    }
}
