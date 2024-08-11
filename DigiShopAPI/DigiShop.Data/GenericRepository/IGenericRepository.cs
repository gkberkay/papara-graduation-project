using System.Linq.Expressions;

namespace DigiShop.Data.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task Save();
        Task<TEntity?> GetById(int id);
        Task<TEntity?> GetByIdAsNoTracking(int id);
        Task<List<TEntity>> GetByIds(List<int> ids);

        Task Insert(TEntity entity);
        Task BulkInsert(List<TEntity> entities);

        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task Delete(int id);
        Task<List<TEntity>> GetAll();
        Task<IQueryable<TEntity>> GetAllAsIQueryable();
        Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> conditions);
        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> conditions);
        Task<TEntity> FirstOrDefaultAsNoTracking(Expression<Func<TEntity, bool>> conditions);
        Task<bool> Any(Expression<Func<TEntity, bool>> conditions);
        Task<TEntity> GetWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> GetAllWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TResult>> GetAllWithIncludeAndSelectAsync<TResult>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selectExpression,
            params Expression<Func<TEntity, object>>[] includes);
    }
}
