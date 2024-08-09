using DigiShop.Schema;
using DigiSopAPI.Base.Response;
using MediatR;

namespace DigiShop.Bussiness.Cqrs
{
        public record CreateUserCommand(UserRequest Request) : IRequest<ApiResponse<UserResponse>>;
        public record UpdateUserCommand(int UserId, UserRequest Request) : IRequest<ApiResponse>;
        public record DeleteUserCommand(int UserId) : IRequest<ApiResponse>;
        public record ValidateUserCommand(UserRequest UserRequest) : IRequest<ApiResponse>;


        public record GetAllUserQuery() : IRequest<ApiResponse<List<UserResponse>>>;
        public record GetUserByIdQuery(int UserId) : IRequest<ApiResponse<UserResponse>>;
        public record GetUserByParameterQuery(string FirstName) : IRequest<ApiResponse<List<UserResponse>>>;

}
