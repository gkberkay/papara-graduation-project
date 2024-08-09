using DigiShop.Data.Domain;
using DigiShop.Data.GenericRepository;

namespace DigiShop.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DigiShopDbContext dbContext;

        public IGenericRepository<Category> CategoryRepository { get; }
        public IGenericRepository<Coupon> CouponRepository { get; }
        public IGenericRepository<Order> OrderRepository { get; }
        public IGenericRepository<OrderDetail> OrderDetailRepository { get; }

        public IGenericRepository<Product> ProductRepository { get; }
        public IGenericRepository<ProductCategory> ProductCategoryRepository { get; }
        public IGenericRepository<User> UserRepository { get; }
        public UnitOfWork(DigiShopDbContext dbContext)
        {
            this.dbContext = dbContext;

            CategoryRepository = new GenericRepository<Category>(this.dbContext);
            CouponRepository = new GenericRepository<Coupon>(this.dbContext);
            OrderRepository = new GenericRepository<Order>(this.dbContext);
            OrderDetailRepository = new GenericRepository<OrderDetail>(this.dbContext);

            ProductRepository = new GenericRepository<Product>(this.dbContext);
            ProductCategoryRepository = new GenericRepository<ProductCategory>(this.dbContext);
            UserRepository = new GenericRepository<User>(this.dbContext);

        }
        public void Dispose()
        {
        }
        public async Task Complete()
        {
            using (var dbTransaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    await dbContext.SaveChangesAsync();
                    await dbTransaction.CommitAsync();
                }
                catch (Exception ex)
                {

                    await dbTransaction.RollbackAsync();
                    Console.WriteLine(ex);
                    throw;
                }
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
