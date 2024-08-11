using AutoMapper;
using DigiShop.Bussiness.Cqrs;
using DigiShop.Data.Domain;
using DigiShop.Data.UnitOfWork;
using DigiShop.Schema;
using DigiSopAPI.Base.Response;
using MediatR;

namespace DigiShop.Bussiness.Command
{
    public class ProductCategoryCommandHandler :
         IRequestHandler<CreateProductCategoryCommand, ApiResponse<ProductCategoryResponse>>,
         IRequestHandler<UpdateProductCategoryCommand, ApiResponse>,
         IRequestHandler<DeleteProductCategoryCommand, ApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ProductCategoryResponse>> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<ProductCategoryRequest, ProductCategory>(request.Request);
            await _unitOfWork.ProductCategoryRepository.Insert(mapped);
            await _unitOfWork.Complete();

            var response = _mapper.Map<ProductCategoryResponse>(mapped);
            return new ApiResponse<ProductCategoryResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<ProductCategoryRequest, ProductCategory>(request.Request);

            _unitOfWork.ProductCategoryRepository.Update(mapped);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.ProductCategoryRepository.Delete(request.Id);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}