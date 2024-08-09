using AutoMapper;
using DigiShop.Data.UnitOfWork;
using DigiSopAPI.Base.Response;
using MediatR;
using DigiShop.Bussiness.Cqrs;
using DigiShop.Data.Domain;
using DigiShop.Schema;

namespace DigiShop.Bussiness.Query;

public class OrderQueryHandler :
    IRequestHandler<GetAllOrderQuery, ApiResponse<List<OrderResponse>>>,
    IRequestHandler<GetOrderByIdQuery, ApiResponse<OrderResponse>>,
    IRequestHandler<GetOrderByParameterQuery, ApiResponse<List<OrderResponse>>>

{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public OrderQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<OrderResponse>>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
    {
        List<Order> entityList = await unitOfWork.OrderRepository.GetAll();
        var mappedList = mapper.Map<List<OrderResponse>>(entityList);
        return new ApiResponse<List<OrderResponse>>(mappedList);
    }

    public async Task<ApiResponse<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.OrderRepository.GetById(request.OrderId);
        var mapped = mapper.Map<OrderResponse>(entity);
        return new ApiResponse<OrderResponse>(mapped);
    }

    public async Task<ApiResponse<List<OrderResponse>>> Handle(GetOrderByParameterQuery request, CancellationToken cancellationToken)
    {
        var users = await unitOfWork.OrderRepository.GetAllWithIncludeAsync(
            x => x.TotalAmount == request.TotalAmount,
            p => p.CouponAmount,
            p => p.User,
            p => p.OrderDetails,
            p => p.OrderNumber,
            p => p.PointsUsed
        );
        var customer = users.FirstOrDefault();
        var mapped = mapper.Map<OrderResponse>(customer);
        var mappedList = new List<OrderResponse> { mapped };
        return new ApiResponse<List<OrderResponse>>(mappedList);
    }
}