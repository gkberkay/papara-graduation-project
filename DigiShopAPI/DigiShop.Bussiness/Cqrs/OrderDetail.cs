using DigiShop.Schema;
using DigiSopAPI.Base.Response;
using MediatR;

namespace DigiShop.Bussiness.Cqrs
{
 
        public record CreateOrderDetailCommand(OrderDetailRequest Request) : IRequest<ApiResponse<OrderDetailResponse>>;
        public record UpdateOrderDetailCommand(int OrderDetailId, OrderDetailRequest Request) : IRequest<ApiResponse>;
        public record DeleteOrderDetailCommand(int OrderDetailId) : IRequest<ApiResponse>;
        public record ValidateOrderDetailCommand(OrderDetailRequest OrderDetailRequest) : IRequest<ApiResponse>;


        public record GetAllOrderDetailQuery() : IRequest<ApiResponse<List<OrderDetailResponse>>>;
        public record GetOrderDetailByIdQuery(int OrderDetailId) : IRequest<ApiResponse<OrderDetailResponse>>;
        public record GetOrderDetailByParameterQuery(string Quantity) : IRequest<ApiResponse<List<OrderDetailResponse>>>;

}
