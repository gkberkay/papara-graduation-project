using DigiShop.Schema;
using DigiSopAPI.Base.Response;
using MediatR;

namespace DigiShop.Bussiness.Cqrs
{

    public record CreateProductCommand(ProductRequest Request) : IRequest<ApiResponse<ProductResponse>>;
    public record UpdateProductCommand(int ProductId, ProductRequest Request) : IRequest<ApiResponse>;
    public record DeleteProductCommand(int ProductId) : IRequest<ApiResponse>;
    public record ValidateProductCommand(ProductRequest ProductRequest) : IRequest<ApiResponse>;


    public record GetAllProductQuery() : IRequest<ApiResponse<List<ProductResponse>>>;
    public record GetProductByIdQuery(int ProductId) : IRequest<ApiResponse<ProductResponse>>;
    public record GetProductByCategoryIdQuery(int CategoryId) : IRequest<ApiResponse<List<ProductResponse>>>;

}
