using DDD.Domain.Entities;
using DDD.Domain.Repositories.Abstract;
using DDD.Infrastructure.EntityFramework.Context.Mssql;
using DDD.Infrastructure.Repository.EntityFramework;

namespace DDD.Infrastructure.Repository
{
    public class CategoryRepository : EntityRepositoryBase<Category, ApplicationDbContext>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
