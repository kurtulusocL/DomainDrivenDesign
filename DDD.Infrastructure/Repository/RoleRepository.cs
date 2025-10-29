using DDD.Domain.Entities.EntityFramework.AppUser;
using DDD.Domain.Repositories.Abstract;
using DDD.Infrastructure.EntityFramework.Context.Mssql;
using DDD.Infrastructure.Repository.EntityFramework;

namespace DDD.Infrastructure.Repository
{
    public class RoleRepository : EntityRepositoryBase<Role, ApplicationDbContext>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
