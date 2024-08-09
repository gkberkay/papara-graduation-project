﻿using System.Linq.Expressions;

namespace DigiShop.Data.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task Save();
        Task<TEntity?> GetById(int Id);
        Task Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task Delete(int Id);
        Task<List<TEntity>> GetAll();
        Task<IQueryable<TEntity>> GetAllAsIQueryable();
        Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> conditions);
        Task<TEntity> GetWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> GetAllWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

    }
}
