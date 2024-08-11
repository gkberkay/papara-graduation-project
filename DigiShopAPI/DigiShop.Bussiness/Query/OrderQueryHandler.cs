using AutoMapper;
using DigiShop.Data.UnitOfWork;
using DigiSopAPI.Base.Response;
using MediatR;
using DigiShop.Bussiness.Cqrs;
using DigiShop.Data.Domain;
using DigiShop.Schema;
using DigiShop.Base.Helpers;
using System.Linq.Expressions;

namespace DigiShop.Bussiness.Query;

public class OrderQueryHandler :
    IRequestHandler<GetOrderQuery, ApiResponse<IEnumerable<OrderResponse>>>,
    IRequestHandler<GetAllOrderQuery, ApiResponse<IEnumerable<OrderResponse>>>,
    IRequestHandler<GetOrderByOrderNoQuery, ApiResponse<OrderResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public OrderQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<OrderResponse>>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var userId = HelperMethods.GetClaimInfo("Id").ToInt();

        var orders = await unitOfWork.OrderRepository.GetAllWithIncludeAndSelectAsync<OrderResponse>(
            p => p.UserId == userId,
            s => new OrderResponse
            {
                Id = s.Id,
                OrderNumber = s.OrderNumber,
                CouponCode = s.CouponCode,
                CouponAmount = s.CouponAmount,
                PointsUsed = s.PointsUsed,
                TotalAmount = s.TotalAmount,
                UserId = s.UserId,
                UserName = s.User.UserName,
                IsActive = s.IsActive,
                OrderDetails = s.OrderDetails
                    .Select(x => new OrderProductResponse
                    {
                        ProductId = x.ProductId,
                        ProductName = x.Product.Name,
                        Quantity = x.Quantity,
                        Price = x.Price
                    })
                .ToList(),
            },
            i => i.User, i => i.OrderDetails
        );

        return new ApiResponse<IEnumerable<OrderResponse>>(orders);
    }

    public async Task<ApiResponse<IEnumerable<OrderResponse>>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Order, bool>> predicate = x => true;

        if (request.IsActive.HasValue)
        {
            predicate = x => x.IsActive == request.IsActive.Value;
        }

        var orders = await unitOfWork.OrderRepository.GetAllWithIncludeAndSelectAsync<OrderResponse>(
            predicate,
            s => new OrderResponse
            {
                Id = s.Id,
                OrderNumber = s.OrderNumber,
                CouponCode = s.CouponCode,
                CouponAmount = s.CouponAmount,
                PointsUsed = s.PointsUsed,
                TotalAmount = s.TotalAmount,
                UserId = s.UserId,
                UserName = s.User.UserName,
                IsActive = s.IsActive,
                OrderDetails = s.OrderDetails
                    .Select(x => new OrderProductResponse
                    {
                        ProductId = x.ProductId,
                        ProductName = x.Product.Name,
                        Quantity = x.Quantity,
                        Price = x.Price
                    })
                .ToList(),
            },
            i => i.User,
            i => i.OrderDetails
        );

        return new ApiResponse<IEnumerable<OrderResponse>>(orders);
    }

    public async Task<ApiResponse<OrderResponse>> Handle(GetOrderByOrderNoQuery request, CancellationToken cancellationToken)
    {
        var orders = await unitOfWork.OrderRepository.GetAllWithIncludeAndSelectAsync<OrderResponse>(
            p => p.OrderNumber == request.OrderNo,
            s => new OrderResponse
            {
                Id = s.Id,
                OrderNumber = s.OrderNumber,
                CouponCode = s.CouponCode,
                CouponAmount = s.CouponAmount,
                PointsUsed = s.PointsUsed,
                TotalAmount = s.TotalAmount,
                UserId = s.UserId,
                UserName = s.User.UserName,
                IsActive = s.IsActive,
                OrderDetails = s.OrderDetails
                    .Select(x => new OrderProductResponse
                    {
                        ProductId = x.ProductId,
                        ProductName = x.Product.Name,
                        Quantity = x.Quantity,
                        Price = x.Price
                    })
                .ToList(),
            },
            i => i.User, i => i.OrderDetails
        );

        return new ApiResponse<OrderResponse>(orders.LastOrDefault());
    }
}