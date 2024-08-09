using DigiShop.Schema;
using DigiSopAPI.Base.Response;
using MediatR;

namespace DigiShop.Bussiness.Cqrs
{
    
        public record CreateCouponCommand(CouponRequest Request) : IRequest<ApiResponse<CouponResponse>>;
        public record UpdateCouponCommand(int CouponId, CouponRequest Request) : IRequest<ApiResponse>;
        public record DeleteCouponCommand(int CouponId) : IRequest<ApiResponse>;
        public record ValidateCouponCommand(CouponRequest CouponRequest) : IRequest<ApiResponse>;


        public record GetAllCouponQuery() : IRequest<ApiResponse<List<CouponResponse>>>;
        public record GetCouponByIdQuery(int CouponId) : IRequest<ApiResponse<CouponResponse>>;
        public record GetCouponByParameterQuery(string Code) : IRequest<ApiResponse<List<CouponResponse>>>;
  
}
