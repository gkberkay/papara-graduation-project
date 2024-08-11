using AutoMapper;
using DigiShop.Data.UnitOfWork;
using DigiSopAPI.Base.Response;
using MediatR;
using DigiShop.Bussiness.Cqrs;
using DigiShop.Data.Domain;
using DigiShop.Schema;

namespace DigiShop.Bussiness.Query;

public class ProductQueryHandler :
    IRequestHandler<GetAllProductQuery, ApiResponse<List<ProductResponse>>>,
    IRequestHandler<GetProductByIdQuery, ApiResponse<ProductResponse>>,
    IRequestHandler<GetProductByCategoryIdQuery, ApiResponse<List<ProductResponse>>>

{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public ProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<ProductResponse>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        List<Product> entityList = await unitOfWork.ProductRepository.GetAll();
        var mappedList = mapper.Map<List<ProductResponse>>(entityList);
        return new ApiResponse<List<ProductResponse>>(mappedList);
    }

    public async Task<ApiResponse<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.ProductRepository.GetById(request.ProductId);
        var mapped = mapper.Map<ProductResponse>(entity);
        return new ApiResponse<ProductResponse>(mapped);
    }

    public async Task<ApiResponse<List<ProductResponse>>> Handle(GetProductByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        var products = await unitOfWork.ProductRepository.GetAllWithIncludeAsync(
            x => x.ProductCategories != null && x.ProductCategories.Any(pc => pc.CategoryId == request.CategoryId),
            x => x.ProductCategories
        );
        var mappedList = mapper.Map<List<ProductResponse>>(products);
        return new ApiResponse<List<ProductResponse>>(mappedList);
    }
}