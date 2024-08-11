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
    public class CategoryCommandHandler :
        IRequestHandler<CreateCategoryCommand, ApiResponse<CategoryResponse>>,
        IRequestHandler<UpdateCategoryCommand, ApiResponse>,
        IRequestHandler<DeleteCategoryCommand, ApiResponse>,
        IRequestHandler<ValidateCategoryCommand, ApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CategoryRequest> _validator;

        public CategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CategoryRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ApiResponse<CategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<CategoryRequest, Category>(request.Request);
            await _unitOfWork.CategoryRepository.Insert(mapped);
            await _unitOfWork.Complete();

            var response = _mapper.Map<CategoryResponse>(mapped);
            return new ApiResponse<CategoryResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<CategoryRequest, Category>(request.Request);
            mapped.Id = request.CategoryId;
            _unitOfWork.CategoryRepository.Update(mapped);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            bool isCategoryUsed = await _unitOfWork.ProductCategoryRepository.Any(x => x.CategoryId == request.CategoryId);

            if (isCategoryUsed)
            {
                return new ApiResponse("This category cannot be deleted because it contains products.");
            }

            await _unitOfWork.CategoryRepository.Delete(request.CategoryId);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(ValidateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.CategoryRequest, cancellationToken);

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
