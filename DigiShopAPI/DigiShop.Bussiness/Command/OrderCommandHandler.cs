using AutoMapper;
using FluentValidation;
using DigiShop.Bussiness.Cqrs;
using DigiShop.Data.Domain;
using DigiShop.Data.UnitOfWork;
using DigiShop.Schema;
using MediatR;
using DigiSopAPI.Base.Response;

namespace DigiShop.Bussiness.Command
{
    public class OrderCommandHandler :
        IRequestHandler<CreateOrderCommand, ApiResponse<OrderResponse>>,
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

        public async Task<ApiResponse<OrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<OrderRequest, Order>(request.Request);
            //mapped.CustomerNumber = new Random().Next(1000000, 9999999);
            await _unitOfWork.OrderRepository.Insert(mapped);
            await _unitOfWork.Complete();

            var response = _mapper.Map<OrderResponse>(mapped);
            return new ApiResponse<OrderResponse>(response);
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
