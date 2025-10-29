using DDD.Domain.Entities.EntityFramework.AppUser;
using DDD.Domain.Repositories.Abstract.BaseRepository;

namespace DDD.Domain.Repositories.Abstract
{
    public interface IRoleRepository : IEntityRepository<Role>
    {
    }
}
