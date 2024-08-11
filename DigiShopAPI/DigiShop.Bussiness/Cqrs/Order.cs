using DigiShop.Schema;
using DigiSopAPI.Base.Response;
using MediatR;

namespace DigiShop.Bussiness.Cqrs
{

    public record CreateOrderCommand(OrderRequest Request) : IRequest<ApiResponse>;
    public record UpdateOrderCommand(int OrderId, OrderRequest Request) : IRequest<ApiResponse>;
    public record DeleteOrderCommand(int OrderId) : IRequest<ApiResponse>;
    public record ValidateOrderCommand(OrderRequest OrderRequest) : IRequest<ApiResponse>;


    public record GetOrderQuery() : IRequest<ApiResponse<IEnumerable<OrderResponse>>>;
    public record GetAllOrderQuery(bool? IsActive = null) : IRequest<ApiResponse<IEnumerable<OrderResponse>>>;
    public record GetOrderByOrderNoQuery(string OrderNo) : IRequest<ApiResponse<OrderResponse>>;

}
