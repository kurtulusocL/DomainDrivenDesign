using DDD.Domain.Entities;
using DDD.Domain.Repositories.Abstract.BaseRepository;

namespace DDD.Domain.Repositories.Abstract
{
    public interface IWriterRepository : IEntityRepository<Writer>
    {
    }
}
