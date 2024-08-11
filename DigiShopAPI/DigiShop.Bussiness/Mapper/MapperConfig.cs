using AutoMapper;
using DigiShop.Schema;
using DigiShop.Data.Domain;

namespace DigiShop.Bussiness.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Category, CategoryResponse>();
            CreateMap<CategoryRequest, Category>();

            CreateMap<Coupon, CouponResponse>();
            CreateMap<CouponRequest, Coupon>();

            CreateMap<Order, OrderResponse>();
            CreateMap<OrderRequest, Order>();

            CreateMap<OrderDetail, OrderDetailResponse>();
            CreateMap<OrderDetailRequest, OrderDetail>();

            CreateMap<Product, ProductResponse>();
            CreateMap<ProductRequest, Product>();

            CreateMap<ProductCategory, ProductCategoryResponse>();
            CreateMap<ProductCategoryRequest, ProductCategory>();

            CreateMap<User, UserResponse>();
            CreateMap<UserRequest, User>();
        }
    }
}
