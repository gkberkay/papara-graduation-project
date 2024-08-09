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

//public class OrderDetailQueryHandler :
//    IRequestHandler<GetAllOrderDetailQuery, ApiResponse<List<OrderDetailResponse>>>,
//    IRequestHandler<GetOrderDetailByIdQuery, ApiResponse<OrderDetailResponse>>,
//    IRequestHandler<GetOrderDetailByParameterQuery, ApiResponse<List<OrderDetailResponse>>>

//{
//    private readonly IUnitOfWork unitOfWork;
//    private readonly IMapper mapper;
//    private readonly ISessionContext sessionContext;

//    public OrderDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionContext sessionContext)
//    {
//        this.unitOfWork = unitOfWork;
//        this.mapper = mapper;
//        this.sessionContext = sessionContext;
//    }

//    public async Task<ApiResponse<List<OrderDetailResponse>>> Handle(GetAllOrderDetailQuery request, CancellationToken cancellationToken)
//    {
//        List<OrderDetail> entityList = await unitOfWork.OrderDetailRepository.GetAll("Customer");
//        var mappedList = mapper.Map<List<OrderDetailResponse>>(entityList);
//        return new ApiResponse<List<OrderDetailResponse>>(mappedList);
//    }

//    public async Task<ApiResponse<OrderDetailResponse>> Handle(GetOrderDetailByIdQuery request, CancellationToken cancellationToken)
//    {
//        var entity = await unitOfWork.OrderDetailRepository.GetById(request.OrderDetailId, "Customer");
//        var mapped = mapper.Map<OrderDetailResponse>(entity);
//        return new ApiResponse<OrderDetailResponse>(mapped);
//    }

//    public async Task<ApiResponse<List<OrderDetailResponse>>> Handle(GetOrderDetailByParameterQuery request, CancellationToken cancellationToken)
//    {
//        List<OrderDetail> entityList = await unitOfWork.OrderDetailRepository.Where(x => x.Id == sessionContext.Session.CustomerId, "Customer");
//        var mappedList = mapper.Map<List<OrderDetailResponse>>(entityList);
//        return new ApiResponse<List<OrderDetailResponse>>(mappedList);
//    }
//}