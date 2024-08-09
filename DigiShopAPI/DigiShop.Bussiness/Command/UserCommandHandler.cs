using AutoMapper;
using DigiShop.Bussiness.Cqrs;
using DigiShop.Data.Domain;
using DigiShop.Data.UnitOfWork;
using DigiShop.Schema;
using DigiSopAPI.Base.Response;
using FluentValidation;
using MediatR;

namespace DigiShop.Bussiness.Command
{
    public class UserCommandHandler :
         IRequestHandler<CreateUserCommand, ApiResponse<UserResponse>>,
         IRequestHandler<UpdateUserCommand, ApiResponse>,
         IRequestHandler<DeleteUserCommand, ApiResponse>,
         IRequestHandler<ValidateUserCommand, ApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<UserRequest> _validator;

        public UserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UserRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ApiResponse<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<UserRequest, User>(request.Request);
            //mapped.CustomerNumber = new Random().Next(1000000, 9999999);
            await _unitOfWork.UserRepository.Insert(mapped);
            await _unitOfWork.Complete();

            var response = _mapper.Map<UserResponse>(mapped);
            return new ApiResponse<UserResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<UserRequest, User>(request.Request);
            mapped.Id = request.UserId;
             _unitOfWork.UserRepository.Update(mapped);
            await _unitOfWork.Complete();
            var response = _mapper.Map<UserResponse>(mapped);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.UserRepository.Delete(request.UserId);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(ValidateUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.UserRequest, cancellationToken);

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
