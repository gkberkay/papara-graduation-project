using DigiShop.Schema;
using Microsoft.AspNetCore.Mvc;
using DigiShop.Bussiness.Cqrs;
using MediatR;
using DigiSopAPI.Base.Response;
using Microsoft.AspNetCore.Authorization;

namespace DigiShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly IMediator mediator;
        public CouponController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<List<CouponResponse>>> GetAll()
        {
            var operation = new GetAllCouponQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{code}")]
        [Authorize]
        public async Task<ApiResponse<CouponResponse>> Get([FromRoute] string code)
        {
            var operation = new GetCouponByCodeQuery(code);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Put(int id, [FromBody] CouponRequest value)
        {
            var operation = new UpdateCouponCommand(id, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<CouponResponse>> Post([FromBody] CouponRequest Coupon)
        {
            var validationOperation = new ValidateCouponCommand(Coupon);
            var operation = new CreateCouponCommand(Coupon);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteCouponCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
