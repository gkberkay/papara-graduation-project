using DigiShop.Base.Entity;
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

        public async Task<TEntity?> GetById(int Id)
        {
            return await dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task Insert(TEntity entity)
        {
            entity.IsActive = true;
            entity.InsertDate = DateTime.UtcNow;
            entity.InsertUser = "System";
            await dbContext.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task Delete(int Id)
        {
            var entity = await dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == Id);
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

        public async Task<TEntity> GetWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return await query.Where(predicate).ToListAsync();
        }
    }
}
