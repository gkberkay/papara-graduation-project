using DigiShop.Schema;
using Microsoft.AspNetCore.Mvc;
using DigiShop.Bussiness.Cqrs;
using MediatR;
using DigiSopAPI.Base.Response;
using Microsoft.AspNetCore.Authorization;
using DigiShop.Base.Helpers;

namespace DigiShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("All")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<List<UserResponse>>> GetAll()
        {
            var operation = new GetAllUserQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet]
        [Authorize]
        public async Task<ApiResponse<UserResponse>> Get()
        {
            var userId = HelperMethods.GetClaimInfo("Id").ToInt();
            var operation = new GetUserByIdQuery(userId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut()]
        [Authorize]
        public async Task<ApiResponse> Put([FromBody] UserRequest user)
        {
            var userId = HelperMethods.GetClaimInfo("Id").ToInt();
            var operation = new UpdateUserCommand(userId, user);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<UserResponse>> Post([FromBody] UserRequest user)
        {
            user.Role = "User";
            var validationOperation = new ValidateUserCommand(user);
            var operation = new CreateUserCommand(user);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost("AddAdmin")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<UserResponse>> AddAdmin([FromBody] UserRequest user)
        {
            user.Role = "Admin";
            var validationOperation = new ValidateUserCommand(user);
            var operation = new CreateUserCommand(user);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteUserCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
