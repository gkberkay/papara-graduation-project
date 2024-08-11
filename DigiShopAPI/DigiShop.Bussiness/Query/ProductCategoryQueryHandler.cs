using AutoMapper;
using DigiShop.Data.UnitOfWork;
using DigiSopAPI.Base.Response;
using MediatR;
using DigiShop.Bussiness.Cqrs;
using DigiShop.Data.Domain;
using DigiShop.Schema;

namespace DigiShop.Bussiness.Query;

public class ProductCategoryQueryHandler :
    IRequestHandler<GetAllProductCategoryQuery, ApiResponse<List<ProductCategoryResponse>>>,
    IRequestHandler<GetProductCategoryByProductIdQuery, ApiResponse<List<ProductCategoryResponse>>>,
    IRequestHandler<GetProductCategoryByCategoryIdQuery, ApiResponse<List<ProductCategoryResponse>>>

{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public ProductCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<ProductCategoryResponse>>> Handle(GetAllProductCategoryQuery request, CancellationToken cancellationToken)
    {
        List<ProductCategory> entities = await unitOfWork.ProductCategoryRepository.GetAll();
        var mappedList = mapper.Map<List<ProductCategoryResponse>>(entities);
        return new ApiResponse<List<ProductCategoryResponse>>(mappedList);
    }

    public async Task<ApiResponse<List<ProductCategoryResponse>>> Handle(GetProductCategoryByProductIdQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.ProductCategoryRepository.Where(x => x.ProductId == request.ProductId);
        var mapped = mapper.Map<List<ProductCategoryResponse>>(entities);
        return new ApiResponse<List<ProductCategoryResponse>>(mapped);
    }

    public async Task<ApiResponse<List<ProductCategoryResponse>>> Handle(GetProductCategoryByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.ProductCategoryRepository.Where(x => x.CategoryId == request.CategoryId);
        var mapped = mapper.Map<List<ProductCategoryResponse>>(entities);
        return new ApiResponse<List<ProductCategoryResponse>>(mapped);
    }
}