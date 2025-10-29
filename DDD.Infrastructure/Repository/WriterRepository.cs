using DDD.Domain.Entities;
using DDD.Domain.Repositories.Abstract;
using DDD.Infrastructure.EntityFramework.Context.Mssql;
using DDD.Infrastructure.Repository.EntityFramework;

namespace DDD.Infrastructure.Repository
{
    public class WriterRepository : EntityRepositoryBase<Writer, ApplicationDbContext>, IWriterRepository
    {
        public WriterRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
