using DDD.Domain.Entities;
using DDD.Domain.Repositories.Abstract;
using DDD.Infrastructure.EntityFramework.Context.Mssql;
using DDD.Infrastructure.Repository.EntityFramework;

namespace DDD.Infrastructure.Repository
{
    public class UserSessionRepository : EntityRepositoryBase<UserSession, ApplicationDbContext>, IUserSessionRepository
    {
        public UserSessionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
