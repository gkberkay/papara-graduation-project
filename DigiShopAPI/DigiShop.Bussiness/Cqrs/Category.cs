using DigiShop.Schema;
using DigiSopAPI.Base.Response;
using MediatR;

namespace DigiShop.Bussiness.Cqrs
{
        public record CreateCategoryCommand(CategoryRequest Request) : IRequest<ApiResponse<CategoryResponse>>;
        public record UpdateCategoryCommand(int CategoryId, CategoryRequest Request) : IRequest<ApiResponse>;
        public record DeleteCategoryCommand(int CategoryId) : IRequest<ApiResponse>;
        public record ValidateCategoryCommand(CategoryRequest CategoryRequest) : IRequest<ApiResponse>;

        public record GetAllCategoryQuery() : IRequest<ApiResponse<List<CategoryResponse>>>;
        public record GetCategoryByIdQuery(int CategoryId) : IRequest<ApiResponse<CategoryResponse>>;
        public record GetCategoryByParameterQuery(string Tags) : IRequest<ApiResponse<List<CategoryResponse>>>;

}
