using DDD.Domain.Entities.EntityFramework.AppUser;
using DDD.Domain.Repositories.Abstract.BaseRepository;

namespace DDD.Domain.Repositories.Abstract
{
    public interface IUserRepository : IEntityRepository<User>
    {
    }
}
