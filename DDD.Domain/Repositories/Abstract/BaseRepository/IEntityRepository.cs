using System.Linq.Expressions;
using DDD.Domain.Entities.EntityFramework.Abstract;

namespace DDD.Domain.Repositories.Abstract.BaseRepository
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task<IEnumerable<T>> GetAllIncludeAsync(Expression<Func<T, bool>>[] filters, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllIncludeByIdAsync(object id, string foreignKeyPropertyName, Expression<Func<T, bool>>[] conditions, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllIncludingByPropertyPathAsync(object id, string foreignKeyPropertyPath, Expression<Func<T, bool>>[] conditions = null, params Expression<Func<T, object>>[] includes);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task<T> GetIncludeAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null);
        IEnumerable<T> GetAllInclude(Expression<Func<T, bool>>[] filters, params Expression<Func<T, object>>[] includes);
        IEnumerable<T> GetAllIncludeById(object id, string foreignKeyPropertyName, Expression<Func<T, bool>>[] conditions, params Expression<Func<T, object>>[] includes);
        IEnumerable<T> GetAllIncludingByPropertyPath(object id, string foreignKeyPropertyPath, Expression<Func<T, bool>>[] conditions = null, params Expression<Func<T, object>>[] includes);
        T Get(Expression<Func<T, bool>> filter);
        T GetInclude(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        Task<T> SetDeletedAsync(Expression<Func<T, bool>> filter);
        Task<T> SetNotDeletedAsync(Expression<Func<T, bool>> filter);
    }
}
