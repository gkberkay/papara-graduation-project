using AutoMapper;
using FluentValidation;
using DigiShop.Bussiness.Cqrs;
using DigiShop.Data.Domain;
using DigiShop.Data.UnitOfWork;
using DigiShop.Schema;
using MediatR;
using DigiSopAPI.Base.Response;
using DigiShop.Base.Helpers;

namespace DigiShop.Bussiness.Command
{
    public class OrderCommandHandler :
        IRequestHandler<CreateOrderCommand, ApiResponse>,
        IRequestHandler<UpdateOrderCommand, ApiResponse>,
        IRequestHandler<DeleteOrderCommand, ApiResponse>,
        IRequestHandler<ValidateOrderCommand, ApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<OrderRequest> _validator;

        public OrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<OrderRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ApiResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var userId = HelperMethods.GetClaimInfo("Id").ToInt();
            var productIds = request.Request.Products.ConvertAll(x => x.ProductId);
            Coupon coupon = null;

            if (request.Request.CouponCode.IsNotNullOrEmpty())
            {
                coupon = await _unitOfWork.CouponRepository.FirstOrDefault(x => x.Code.Equals(request.Request.CouponCode) && x.IsActive && x.ExpiredDate > DateTime.Now);

                if (coupon == null)
                {
                    return new ApiResponse("No coupon was found for the entered coupon code");
                }
            }

            request.Request.UserId = userId;

            var user = await _unitOfWork.UserRepository.GetById(request.Request.UserId);
            var products = await _unitOfWork.ProductRepository.GetByIds(productIds);

            Order order = new Order()
            {
                UserId = userId,
                TotalAmount = products.Sum(x => x.Price * request.Request.Products.FirstOrDefault(rp => rp.ProductId == x.Id)?.Quantity ?? 0),
                CouponCode = coupon?.Code,
                OrderNumber = HelperMethods.GenerateRandomText()
            };

            ApplyCouponAndPoints(user, coupon, order);
            CalculatePointsBalance(products, request.Request.Products, user, order);

            await _unitOfWork.OrderRepository.Insert(order);
            await _unitOfWork.Complete();

            return new ApiResponse($"Order number {order.OrderNumber} has been created.",true);
        }

        private void CalculatePointsBalance(List<Product> products, List<OrderProduct> orderProducts, User user, Order order)
        {
            order.OrderDetails = new List<OrderDetail>();

            foreach (var product in products)
            {
                var quantity = orderProducts.Where(x => x.ProductId == product.Id)?.FirstOrDefault()?.Quantity ?? 0;
                var point = (product.Price * quantity) * (product.PointsPercentage / 100);
                user.PointsBalance += point > product.MaxPoints ? product.MaxPoints : point;
                product.StockCount -= quantity;
                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = product.Id,
                    Quantity = quantity,
                    Price = product.Price
                });
            }
        }

        private void ApplyCouponAndPoints(User user, Coupon coupon, Order order)
        {
            var totalAmount = order.TotalAmount;
            if (coupon != null)
            {
                if (coupon.SalePrice >= totalAmount)
                {
                    order.CouponAmount = totalAmount;
                    totalAmount = 0;
                    user.PointsBalance += coupon.SalePrice - order.CouponAmount;
                }
                else
                {
                    order.CouponAmount = coupon.SalePrice;
                    totalAmount -= coupon.SalePrice;
                }
                coupon.IsActive = false;
            }
            if (user.PointsBalance > 0 && totalAmount > 0)
            {
                if (user.PointsBalance >= totalAmount)
                {
                    order.PointsUsed = totalAmount;
                    user.PointsBalance -= totalAmount;
                    totalAmount = 0;
                }
                else
                {
                    order.PointsUsed = user.PointsBalance;
                    totalAmount -= user.PointsBalance;
                    user.PointsBalance = 0;
                }
            }
        }

        public async Task<ApiResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<OrderRequest, Order>(request.Request);
            mapped.Id = request.OrderId;
            _unitOfWork.OrderRepository.Update(mapped);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.OrderRepository.Delete(request.OrderId);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(ValidateOrderCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.OrderRequest, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errorResponse = new ApiResponse()
                {
                    Message = validationResult.Errors.FirstOrDefault()?.ErrorMessage
                };
                return errorResponse;
            }

            return new ApiResponse();
        }
    }
}
