using System.Linq.Expressions;
using DDD.Domain.Entities.EntityFramework.Abstract;
using DDD.Domain.Repositories.Abstract.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace DDD.Infrastructure.Repository.EntityFramework
{
    public class EntityRepositoryBase<T, TContext> : IEntityRepository<T> where T : class, IEntity, new() where TContext : DbContext
    {
        private readonly TContext _context;
        public EntityRepositoryBase(TContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            return filter == null
            ? await _context.Set<T>().ToListAsync()
            : await _context.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllIncludeAsync(Expression<Func<T, bool>>[] filters, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();
                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        if (include != null)
                        {
                            query = query.Include(include);
                        }
                    }
                }

                if (filters != null)
                {
                    foreach (var filter in filters)
                    {
                        if (filter != null)
                        {
                            query = query.Where(filter);
                        }
                    }
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving entities with related data.", ex);
            }
        }

        public async Task<IEnumerable<T>> GetAllIncludeByIdAsync(object id, string foreignKeyPropertyName, Expression<Func<T, bool>>[] conditions, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();
                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }

                if (conditions != null)
                {
                    foreach (var condition in conditions)
                    {
                        if (condition != null)
                        {
                            query = query.Where(condition);
                        }
                    }
                }

                if (id != null && !string.IsNullOrWhiteSpace(foreignKeyPropertyName))
                {
                    var parameter = Expression.Parameter(typeof(T), "entity");
                    var foreignKeyProperty = Expression.Property(parameter, foreignKeyPropertyName);
                    var propertyType = foreignKeyProperty.Type;

                    if (propertyType == typeof(int?) && id is int intId)
                    {
                        var idExpression = Expression.Convert(Expression.Constant(intId), typeof(int?));
                        query = query.Where(Expression.Lambda<Func<T, bool>>(
                            Expression.Equal(foreignKeyProperty, idExpression),
                            parameter
                        ));
                    }
                    else if (propertyType == typeof(int) && id is int intIdNonNullable)
                    {
                        query = query.Where(Expression.Lambda<Func<T, bool>>(
                            Expression.Equal(foreignKeyProperty, Expression.Constant(intIdNonNullable)),
                            parameter
                        ));
                    }
                    else if (propertyType == typeof(string) && id is string strId)
                    {
                        query = query.Where(Expression.Lambda<Func<T, bool>>(
                            Expression.Equal(foreignKeyProperty, Expression.Constant(strId)),
                            parameter
                        ));
                    }
                    else
                    {
                        throw new ArgumentException($"Unsupported ID type: {id.GetType()}");
                    }
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving entities with related data.", ex);
            }
        }

        public async Task<IEnumerable<T>> GetAllIncludingByPropertyPathAsync(object id, string foreignKeyPropertyPath, Expression<Func<T, bool>>[] conditions = null, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }

                if (conditions != null)
                {
                    foreach (var condition in conditions)
                    {
                        if (condition != null)
                        {
                            query = query.Where(condition);
                        }
                    }
                }

                if (id != null && !string.IsNullOrWhiteSpace(foreignKeyPropertyPath))
                {
                    var parameter = Expression.Parameter(typeof(T), "entity");
                    Expression propertyExpression = parameter;

                    foreach (var propertyName in foreignKeyPropertyPath.Split('.'))
                    {
                        propertyExpression = Expression.PropertyOrField(propertyExpression, propertyName);
                    }

                    var propertyType = propertyExpression.Type;

                    if (propertyType == typeof(int) && id is int intId)
                    {
                        var idExpression = Expression.Convert(Expression.Constant(intId), typeof(int));
                        query = query.Where(Expression.Lambda<Func<T, bool>>(
                            Expression.Equal(propertyExpression, idExpression), parameter));
                    }
                    else if (propertyType == typeof(int?) && id is int intIdNullable)
                    {
                        var idExpression = Expression.Convert(Expression.Constant(intIdNullable), typeof(int?));
                        query = query.Where(Expression.Lambda<Func<T, bool>>(
                            Expression.Equal(propertyExpression, idExpression), parameter));
                    }
                    else if (propertyType == typeof(string) && id is string strId)
                    {
                        var idExpression = Expression.Constant(strId);
                        query = query.Where(Expression.Lambda<Func<T, bool>>(
                            Expression.Equal(propertyExpression, idExpression), parameter));
                    }
                    else
                    {
                        throw new ArgumentException($"Unsupported ID type: {id.GetType()} for property path {foreignKeyPropertyPath}");
                    }
                }

                return await Task.FromResult(query);
            }
            catch (Exception)
            {
                return await Task.FromResult(_context.Set<T>().AsQueryable());
            }
        }

        public async Task<T> GetIncludeAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter), "Filter cannot be null for GetAsync method.");
            }

            try
            {
                IQueryable<T> query = _context.Set<T>();

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
                return await query.FirstOrDefaultAsync(filter);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the entity with related data.", ex);
            }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter), "Filter cannot be null for GetAsync method.");
            }
            return await _context.Set<T>().FirstOrDefaultAsync(filter);
        }

        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "Entity cannot be null for AddAsync method.");
                }
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while adding the entity. Please check the database connection or constraints.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "Entity cannot be null for UpdateAsync method.");
                }
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while updating the entity. Please check the database connection or constraints.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "Entity cannot be null for DeleteAsync method.");
                }

                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while deleting the entity. Please check the database connection or constraints.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            return filter == null
             ? _context.Set<T>().ToList()
             : _context.Set<T>().Where(filter).ToList();
        }

        public IEnumerable<T> GetAllInclude(Expression<Func<T, bool>>[] filters, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();
                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        if (include != null)
                        {
                            query = query.Include(include);
                        }
                    }
                }

                if (filters != null)
                {
                    foreach (var filter in filters)
                    {
                        if (filter != null)
                        {
                            query = query.Where(filter);
                        }
                    }
                }
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving entities with related data.", ex);
            }
        }

        public IEnumerable<T> GetAllIncludeById(object id, string foreignKeyPropertyName, Expression<Func<T, bool>>[] conditions, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();
                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }

                if (conditions != null)
                {
                    foreach (var condition in conditions)
                    {
                        if (condition != null)
                        {
                            query = query.Where(condition);
                        }
                    }
                }

                if (id != null && !string.IsNullOrWhiteSpace(foreignKeyPropertyName))
                {
                    var parameter = Expression.Parameter(typeof(T), "entity");
                    var foreignKeyProperty = Expression.Property(parameter, foreignKeyPropertyName);
                    var propertyType = foreignKeyProperty.Type;

                    if (propertyType == typeof(int?) && id is int intId)
                    {
                        var idExpression = Expression.Convert(Expression.Constant(intId), typeof(int?));
                        query = query.Where(Expression.Lambda<Func<T, bool>>(
                            Expression.Equal(foreignKeyProperty, idExpression),
                            parameter
                        ));
                    }
                    else if (propertyType == typeof(int) && id is int intIdNonNullable)
                    {
                        query = query.Where(Expression.Lambda<Func<T, bool>>(
                            Expression.Equal(foreignKeyProperty, Expression.Constant(intIdNonNullable)),
                            parameter
                        ));
                    }
                    else if (propertyType == typeof(string) && id is string strId)
                    {
                        query = query.Where(Expression.Lambda<Func<T, bool>>(
                            Expression.Equal(foreignKeyProperty, Expression.Constant(strId)),
                            parameter
                        ));
                    }
                    else
                    {
                        throw new ArgumentException($"Unsupported ID type: {id.GetType()}");
                    }
                }
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving entities with related data.", ex);
            }
        }

        public IEnumerable<T> GetAllIncludingByPropertyPath(object id, string foreignKeyPropertyPath, Expression<Func<T, bool>>[] conditions = null, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }

                if (conditions != null)
                {
                    foreach (var condition in conditions)
                    {
                        if (condition != null)
                        {
                            query = query.Where(condition);
                        }
                    }
                }

                if (id != null && !string.IsNullOrWhiteSpace(foreignKeyPropertyPath))
                {
                    var parameter = Expression.Parameter(typeof(T), "entity");
                    Expression propertyExpression = parameter;

                    foreach (var propertyName in foreignKeyPropertyPath.Split('.'))
                    {
                        propertyExpression = Expression.PropertyOrField(propertyExpression, propertyName);
                    }

                    var propertyType = propertyExpression.Type;

                    if (propertyType == typeof(int) && id is int intId)
                    {
                        var idExpression = Expression.Convert(Expression.Constant(intId), typeof(int));
                        query = query.Where(Expression.Lambda<Func<T, bool>>(
                            Expression.Equal(propertyExpression, idExpression), parameter));
                    }
                    else if (propertyType == typeof(int?) && id is int intIdNullable)
                    {
                        var idExpression = Expression.Convert(Expression.Constant(intIdNullable), typeof(int?));
                        query = query.Where(Expression.Lambda<Func<T, bool>>(
                            Expression.Equal(propertyExpression, idExpression), parameter));
                    }
                    else if (propertyType == typeof(string) && id is string strId)
                    {
                        var idExpression = Expression.Constant(strId);
                        query = query.Where(Expression.Lambda<Func<T, bool>>(
                            Expression.Equal(propertyExpression, idExpression), parameter));
                    }
                    else
                    {
                        throw new ArgumentException($"Unsupported ID type: {id.GetType()} for property path {foreignKeyPropertyPath}");
                    }
                }

                return query;
            }
            catch (Exception)
            {
                return _context.Set<T>().AsQueryable();
            }
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter), "Filter cannot be null for Get method.");
            }
            return _context.Set<T>().FirstOrDefault(filter);
        }

        public T GetInclude(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter), "Filter cannot be null for GetAsync method.");
            }

            try
            {
                IQueryable<T> query = _context.Set<T>();

                foreach (var include in includes)
                {
                    query = query.Include(include).AsNoTracking();
                }
                return query.FirstOrDefault(filter);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the entity with related data.", ex);
            }
        }
        public bool Add(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "Entity cannot be null for Add method.");
                }
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while adding the entity. Please check the database connection or constraints.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<T> SetDeletedAsync(Expression<Func<T, bool>> filter)
        {
            try
            {
                var entity = await _context.Set<T>().FirstOrDefaultAsync(filter);
                if (entity == null) return null;

                if (entity is IDeletable deletable)
                {
                    deletable.IsDeleted = true;
                    deletable.DeletedDate = DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting deleted the entity.", ex);
            }
        }

        public async Task<T> SetNotDeletedAsync(Expression<Func<T, bool>> filter)
        {
            try
            {
                var entity = await _context.Set<T>().FirstOrDefaultAsync(filter);
                if (entity == null) return null;

                if (entity is IDeletable deletable)
                {
                    deletable.IsDeleted = false;
                    deletable.DeletedDate = null;
                }

                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting not deleted the entity.", ex);
            }
        }
    }
}
