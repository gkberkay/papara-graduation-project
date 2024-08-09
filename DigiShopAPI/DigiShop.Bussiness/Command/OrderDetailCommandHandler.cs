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
    public class OrderDetailCommandHandler :
        IRequestHandler<CreateOrderDetailCommand, ApiResponse<OrderDetailResponse>>,
        IRequestHandler<UpdateOrderDetailCommand, ApiResponse>,
        IRequestHandler<DeleteOrderDetailCommand, ApiResponse>,
        IRequestHandler<ValidateOrderDetailCommand, ApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<OrderDetailRequest> _validator;

        public OrderDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<OrderDetailRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ApiResponse<OrderDetailResponse>> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<OrderDetailRequest, OrderDetail>(request.Request);
            //mapped.CustomerNumber = new Random().Next(1000000, 9999999);
            await _unitOfWork.OrderDetailRepository.Insert(mapped);
            await _unitOfWork.Complete();

            var response = _mapper.Map<OrderDetailResponse>(mapped);
            return new ApiResponse<OrderDetailResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<OrderDetailRequest, OrderDetail>(request.Request);
            mapped.Id = request.OrderDetailId;
            _unitOfWork.OrderDetailRepository.Update(mapped);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteOrderDetailCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.OrderDetailRepository.Delete(request.OrderDetailId);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(ValidateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.OrderDetailRequest, cancellationToken);

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
