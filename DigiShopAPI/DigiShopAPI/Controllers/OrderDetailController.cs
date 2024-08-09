using DigiShop.Schema;
using Microsoft.AspNetCore.Mvc;
using DigiShop.Bussiness.Cqrs;
using MediatR;
using DigiSopAPI.Base.Response;

namespace DigiShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IMediator mediator;
        public OrderDetailController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<OrderDetailResponse>>> GetAll()
        {
            var operation = new GetAllOrderDetailQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<OrderDetailResponse>> Get([FromRoute] int id)
        {
            var operation = new GetOrderDetailByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] OrderDetailRequest value)
        {
            var operation = new UpdateOrderDetailCommand(id, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<OrderDetailResponse>> Post([FromBody] OrderDetailRequest OrderDetail)
        {
            var validationOperation = new ValidateOrderDetailCommand(OrderDetail);
            var operation = new CreateOrderDetailCommand(OrderDetail);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{Id}")]
        public async Task<ApiResponse> Delete(int Id)
        {
            var operation = new DeleteOrderDetailCommand(Id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
