using DigiShop.Schema;
using DigiSopAPI.Base.Response;
using MediatR;

namespace DigiShop.Bussiness.Cqrs
{
    
        public record CreateOrderCommand(OrderRequest Request) : IRequest<ApiResponse<OrderResponse>>;
        public record UpdateOrderCommand(int OrderId, OrderRequest Request) : IRequest<ApiResponse>;
        public record DeleteOrderCommand(int OrderId) : IRequest<ApiResponse>;
        public record ValidateOrderCommand(OrderRequest OrderRequest) : IRequest<ApiResponse>;


        public record GetAllOrderQuery() : IRequest<ApiResponse<List<OrderResponse>>>;
        public record GetOrderByIdQuery(int OrderId) : IRequest<ApiResponse<OrderResponse>>;
        public record GetOrderByParameterQuery(int TotalAmount) : IRequest<ApiResponse<List<OrderResponse>>>;

}
