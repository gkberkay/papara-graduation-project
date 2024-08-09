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
    IRequestHandler<GetProductByParameterQuery, ApiResponse<List<ProductResponse>>>

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

    public async Task<ApiResponse<List<ProductResponse>>> Handle(GetProductByParameterQuery request, CancellationToken cancellationToken)
    {
        var users = await unitOfWork.ProductRepository.GetAllWithIncludeAsync(
            x => x.Name == request.Name,
            p => p.OrderDetails,
            p => p.ProductCategories,
            p => p.Description,
            p => p.PointsPercentage,
            p => p.StockCount,
            p => p.MaxPoints
        );
        var customer = users.FirstOrDefault();
        var mapped = mapper.Map<ProductResponse>(customer);
        var mappedList = new List<ProductResponse> { mapped };
        return new ApiResponse<List<ProductResponse>>(mappedList);
    }
}