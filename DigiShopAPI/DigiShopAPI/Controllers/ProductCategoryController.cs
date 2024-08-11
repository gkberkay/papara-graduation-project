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
    public class ProductCategoryController : ControllerBase
    {
        private readonly IMediator mediator;
        public ProductCategoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<List<ProductCategoryResponse>>> GetAll()
        {
            var operation = new GetAllProductCategoryQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetByProductId/{id}")]
        [Authorize]
        public async Task<ApiResponse<List<ProductCategoryResponse>>> GetByProductId([FromRoute] int id)
        {
            var operation = new GetProductCategoryByProductIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetByCategoryId/{id}")]
        [Authorize]
        public async Task<ApiResponse<List<ProductCategoryResponse>>> GetByCategoryId([FromRoute] int id)
        {
            var operation = new GetProductCategoryByCategoryIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Put(int id, [FromBody] ProductCategoryRequest value)
        {
            var operation = new UpdateProductCategoryCommand(id, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<ProductCategoryResponse>> Post([FromBody] ProductCategoryRequest ProductCategory)
        {
            var operation = new CreateProductCategoryCommand(ProductCategory);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteProductCategoryCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}