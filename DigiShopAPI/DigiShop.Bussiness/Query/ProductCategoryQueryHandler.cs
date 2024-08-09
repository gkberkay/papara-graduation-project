//using AutoMapper;
//using DigiShop.Data.UnitOfWork;
//using DigiSopAPI.Base.Response;
//using MediatR;
//using DigiShop.Base;
//using DigiShop.Base.Response;
//using DigiShop.Bussiness.Cqrs;
//using DigiShop.Data.Domain;
//using DigiShop.Data.UnitOfWork;
//using DigiShop.Schema;

//namespace DigiShop.Bussiness.Query;

//public class ProductCategoryQueryHandler :
//    IRequestHandler<GetAllProductCategoryQuery, ApiResponse<List<ProductCategoryResponse>>>,
//    IRequestHandler<GetProductCategoryByIdQuery, ApiResponse<ProductCategoryResponse>>,
//    IRequestHandler<GetProductCategoryByParameterQuery, ApiResponse<List<ProductCategoryResponse>>>

//{
//    private readonly IUnitOfWork unitOfWork;
//    private readonly IMapper mapper;
//    private readonly ISessionContext sessionContext;

//    public ProductCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionContext sessionContext)
//    {
//        this.unitOfWork = unitOfWork;
//        this.mapper = mapper;
//        this.sessionContext = sessionContext;
//    }

//    public async Task<ApiResponse<List<ProductCategoryResponse>>> Handle(GetAllProductCategoryQuery request, CancellationToken cancellationToken)
//    {
//        List<ProductCategory> entityList = await unitOfWork.ProductCategoryRepository.GetAll("Customer");
//        var mappedList = mapper.Map<List<ProductCategoryResponse>>(entityList);
//        return new ApiResponse<List<ProductCategoryResponse>>(mappedList);
//    }

//    public async Task<ApiResponse<ProductCategoryResponse>> Handle(GetProductCategoryByIdQuery request, CancellationToken cancellationToken)
//    {
//        var entity = await unitOfWork.ProductCategoryRepository.GetById(request.ProductCategoryId, "Customer");
//        var mapped = mapper.Map<ProductCategoryResponse>(entity);
//        return new ApiResponse<ProductCategoryResponse>(mapped);
//    }

//    public async Task<ApiResponse<List<ProductCategoryResponse>>> Handle(GetProductCategoryByParameterQuery request, CancellationToken cancellationToken)
//    {
//        List<ProductCategory> entityList = await unitOfWork.ProductCategoryRepository.Where(x => x.Id == sessionContext.Session.CustomerId, "Customer");
//        var mappedList = mapper.Map<List<ProductCategoryResponse>>(entityList);
//        return new ApiResponse<List<ProductCategoryResponse>>(mappedList);
//    }
//}