using DigiShop.Schema;
using Microsoft.AspNetCore.Mvc;
using DigiShop.Bussiness.Cqrs;
using MediatR;
using DigiSopAPI.Base.Response;

namespace DigiShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator mediator;
        public OrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<OrderResponse>>> GetAll()
        {
            var operation = new GetAllOrderQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<OrderResponse>> Get([FromRoute] int id)
        {
            var operation = new GetOrderByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] OrderRequest value)
        {
            var operation = new UpdateOrderCommand(id, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<OrderResponse>> Post([FromBody] OrderRequest Order)
        {
            var validationOperation = new ValidateOrderCommand(Order);
            var operation = new CreateOrderCommand(Order);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{Id}")]
        public async Task<ApiResponse> Delete(int Id)
        {
            var operation = new DeleteOrderCommand(Id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
