using DigiShop.Data.Domain;
using DigiShop.Data.GenericRepository;

namespace DigiShop.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task Complete();
        IGenericRepository<Category> CategoryRepository { get; }
        IGenericRepository<Coupon> CouponRepository { get; }
        IGenericRepository<Order> OrderRepository { get; }
        IGenericRepository<OrderDetail> OrderDetailRepository { get; }

        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<ProductCategory> ProductCategoryRepository { get; }
        IGenericRepository<User> UserRepository { get; }
    }
}
