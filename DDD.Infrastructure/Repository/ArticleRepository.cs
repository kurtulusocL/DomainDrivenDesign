using DDD.Domain.Entities;
using DDD.Domain.Repositories.Abstract;
using DDD.Infrastructure.EntityFramework.Context.Mssql;
using DDD.Infrastructure.Repository.EntityFramework;

namespace DDD.Infrastructure.Repository
{
    public class ArticleRepository : EntityRepositoryBase<Article, ApplicationDbContext>, IArticleRepository
    {
        public ArticleRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
