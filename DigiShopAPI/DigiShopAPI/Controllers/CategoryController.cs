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
    public class CategoryController : ControllerBase
    {
        private readonly IMediator mediator;
        public CategoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<ApiResponse<List<CategoryResponse>>> GetAll()
        {
            var operation = new GetAllCategoryQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<CategoryResponse>> Get([FromRoute] int id)
        {
            var operation = new GetCategoryByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Put(int id, [FromBody] CategoryRequest value)
        {
            var operation = new UpdateCategoryCommand(id, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<CategoryResponse>> Post([FromBody] CategoryRequest Category)
        {
            var validationOperation = new ValidateCategoryCommand(Category);
            var operation = new CreateCategoryCommand(Category);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteCategoryCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
