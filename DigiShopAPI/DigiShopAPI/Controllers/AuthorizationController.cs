using DigiShop.Bussiness.Cqrs;
using DigiShop.Schema;
using DigiSopAPI.Base.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigiShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthorizationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse<AuthorizationResponse>> Post([FromBody] AuthorizationRequest value)
        {
            var operation = new CreateAuthorizationTokenCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

    }
}
