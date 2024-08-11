using DigiShop.Base.Entity;
using DigiShop.Base.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DigiShop.Data.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DigiShopDbContext dbContext;
        public GenericRepository(DigiShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Save()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task<TEntity?> GetById(int id)
        {
            return await dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TEntity?> GetByIdAsNoTracking(int id)
        {
            return await dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<List<TEntity>> GetByIds(List<int> ids)
        {
            return await dbContext.Set<TEntity>().Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task Insert(TEntity entity)
        {
            entity.InsertDate = DateTime.UtcNow;
            entity.InsertUser = HelperMethods.GetClaimInfo("UserName").IsNull("System");
            await dbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task BulkInsert(List<TEntity> entities)
        {
            entities.ForEach(entity =>
            {
                entity.InsertUser = HelperMethods.GetClaimInfo("UserName").IsNull("System");
                entity.InsertDate = DateTime.UtcNow;
            });

            await dbContext.Set<TEntity>().AddRangeAsync(entities);
        }

        public void Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
            if (entity is not null)
                dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IQueryable<TEntity>> GetAllAsIQueryable()
        {
            return dbContext.Set<TEntity>().AsQueryable();
        }

        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> conditions)
        {
            return await dbContext.Set<TEntity>().AsNoTracking().Where(conditions).ToListAsync();
        }

        public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> conditions)
        {
            return await dbContext.Set<TEntity>().Where(conditions).FirstOrDefaultAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsNoTracking(Expression<Func<TEntity, bool>> conditions)
        {
            return await dbContext.Set<TEntity>().AsNoTracking().Where(conditions).FirstOrDefaultAsync();
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>> conditions)
        {
            return await dbContext.Set<TEntity>().AsNoTracking().AnyAsync(conditions);
        }

        public async Task<TEntity> GetWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return await query.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return await query.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TResult>> GetAllWithIncludeAndSelectAsync<TResult>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selectExpression,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return await query.AsNoTracking().Where(predicate).Select(selectExpression).ToListAsync();
        }
    }
}
