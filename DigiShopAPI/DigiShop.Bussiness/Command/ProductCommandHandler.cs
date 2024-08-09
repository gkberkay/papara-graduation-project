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
    public class ProductCommandHandler :
        IRequestHandler<CreateProductCommand, ApiResponse<ProductResponse>>,
        IRequestHandler<UpdateProductCommand, ApiResponse>,
        IRequestHandler<DeleteProductCommand, ApiResponse>,
        IRequestHandler<ValidateProductCommand, ApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductRequest> _validator;

        public ProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ProductRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ApiResponse<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<ProductRequest, Product>(request.Request);
            //mapped.CustomerNumber = new Random().Next(1000000, 9999999);
            await _unitOfWork.ProductRepository.Insert(mapped);
            await _unitOfWork.Complete();

            var response = _mapper.Map<ProductResponse>(mapped);
            return new ApiResponse<ProductResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<ProductRequest, Product>(request.Request);
            mapped.Id = request.ProductId;
            _unitOfWork.ProductRepository.Update(mapped);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.ProductRepository.Delete(request.ProductId);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(ValidateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.ProductRequest, cancellationToken);

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
