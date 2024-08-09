using DigiShop.Schema;
using Microsoft.AspNetCore.Mvc;
using DigiShop.Bussiness.Cqrs;
using MediatR;
using DigiSopAPI.Base.Response;

namespace DigiShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;
        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<ProductResponse>>> GetAll()
        {
            var operation = new GetAllProductQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<ProductResponse>> Get([FromRoute] int id)
        {
            var operation = new GetProductByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] ProductRequest value)
        {
            var operation = new UpdateProductCommand(id, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<ProductResponse>> Post([FromBody] ProductRequest Product)
        {
            var validationOperation = new ValidateProductCommand(Product);
            var operation = new CreateProductCommand(Product);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{Id}")]
        public async Task<ApiResponse> Delete(int Id)
        {
            var operation = new DeleteProductCommand(Id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
