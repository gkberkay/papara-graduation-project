using DigiShop.Schema;
using DigiSopAPI.Base.Response;
using MediatR;

namespace DigiShop.Bussiness.Cqrs
{
    public record CreateProductCategoryCommand(ProductCategoryRequest Request) : IRequest<ApiResponse<ProductCategoryResponse>>;
    public record UpdateProductCategoryCommand(int Id, ProductCategoryRequest Request) : IRequest<ApiResponse>;
    public record DeleteProductCategoryCommand(int Id) : IRequest<ApiResponse>;

    public record GetAllProductCategoryQuery() : IRequest<ApiResponse<List<ProductCategoryResponse>>>;
    public record GetProductCategoryByProductIdQuery(int ProductId) : IRequest<ApiResponse<List<ProductCategoryResponse>>>;
    public record GetProductCategoryByCategoryIdQuery(int CategoryId) : IRequest<ApiResponse<List<ProductCategoryResponse>>>;
}