using DDD.Domain.Entities;
using DDD.Domain.Repositories.Abstract;
using DDD.Infrastructure.EntityFramework.Context.Mssql;
using DDD.Infrastructure.Repository.EntityFramework;

namespace DDD.Infrastructure.Repository
{
    public class ExceptionLoggerRepository : EntityRepositoryBase<ExceptionLogger, ApplicationDbContext>, IExceptionLoggerRepository
    {
        public ExceptionLoggerRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
