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
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IMediator mediator;
        public OrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<OrderResponse>>> Get()
        {
            var operation = new GetOrderQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("All")]
        public async Task<ApiResponse<IEnumerable<OrderResponse>>> GetAll(bool? isActive = null)
        {
            var operation = new GetAllOrderQuery(isActive);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{orderNo}")]
        public async Task<ApiResponse<OrderResponse>> GetByOrderNo([FromRoute] string orderNo)
        {
            var operation = new GetOrderByOrderNoQuery(orderNo);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse> Post([FromBody] OrderRequest order)
        {
            var validationOperation = new ValidateOrderCommand(order);
            var operation = new CreateOrderCommand(order);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteOrderCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
