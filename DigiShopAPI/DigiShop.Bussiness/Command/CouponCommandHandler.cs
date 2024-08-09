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
    public class CouponCommandHandler :
        IRequestHandler<CreateCouponCommand, ApiResponse<CouponResponse>>,
        IRequestHandler<UpdateCouponCommand, ApiResponse>,
        IRequestHandler<DeleteCouponCommand, ApiResponse>,
        IRequestHandler<ValidateCouponCommand, ApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CouponRequest> _validator;

        public CouponCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CouponRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ApiResponse<CouponResponse>> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<CouponRequest, Coupon>(request.Request);
            //mapped.CustomerNumber = new Random().Next(1000000, 9999999);
            await _unitOfWork.CouponRepository.Insert(mapped);
            await _unitOfWork.Complete();

            var response = _mapper.Map<CouponResponse>(mapped);
            return new ApiResponse<CouponResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<CouponRequest, Coupon>(request.Request);
            mapped.Id = request.CouponId;
            _unitOfWork.CouponRepository.Update(mapped);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.CouponRepository.Delete(request.CouponId);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(ValidateCouponCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.CouponRequest, cancellationToken);

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
