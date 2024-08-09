using AutoMapper;
using DigiShop.Data.UnitOfWork;
using DigiSopAPI.Base.Response;
using MediatR;
using DigiShop.Bussiness.Cqrs;
using DigiShop.Data.Domain;
using DigiShop.Schema;

namespace DigiShop.Bussiness.Query;

public class CategoryQueryHandler :
    IRequestHandler<GetAllCategoryQuery, ApiResponse<List<CategoryResponse>>>,
    IRequestHandler<GetCategoryByIdQuery, ApiResponse<CategoryResponse>>,
    IRequestHandler<GetCategoryByParameterQuery, ApiResponse<List<CategoryResponse>>>

{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<CategoryResponse>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        List<Category> entityList = await unitOfWork.CategoryRepository.GetAll();
        var mappedList = mapper.Map<List<CategoryResponse>>(entityList);
        return new ApiResponse<List<CategoryResponse>>(mappedList);
    }

    public async Task<ApiResponse<CategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.CategoryRepository.GetById(request.CategoryId);
        var mapped = mapper.Map<CategoryResponse>(entity);
        return new ApiResponse<CategoryResponse>(mapped);
    }

    public async Task<ApiResponse<List<CategoryResponse>>> Handle(GetCategoryByParameterQuery request, CancellationToken cancellationToken)
    {
        var Categorys = await unitOfWork.CategoryRepository.GetAllWithIncludeAsync(
            x => x.Tags == request.Tags
        );
        var customer = Categorys.FirstOrDefault();
        var mapped = mapper.Map<CategoryResponse>(customer);
        var mappedList = new List<CategoryResponse> { mapped };
        return new ApiResponse<List<CategoryResponse>>(mappedList);
    }
}