using AutoMapper;
using DigiShop.Data.UnitOfWork;
using DigiSopAPI.Base.Response;
using MediatR;
using DigiShop.Bussiness.Cqrs;
using DigiShop.Data.Domain;
using DigiShop.Schema;

namespace DigiShop.Bussiness.Query;

public class UserQueryHandler :
    IRequestHandler<GetAllUserQuery, ApiResponse<List<UserResponse>>>,
    IRequestHandler<GetUserByIdQuery, ApiResponse<UserResponse>>,
    IRequestHandler<GetUserByParameterQuery, ApiResponse<List<UserResponse>>>

{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public UserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<UserResponse>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        List<User> entityList = await unitOfWork.UserRepository.GetAll();
        var mappedList = mapper.Map<List<UserResponse>>(entityList);
        return new ApiResponse<List<UserResponse>>(mappedList);
    }

    public async Task<ApiResponse<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.UserRepository.GetById(request.UserId);
        var mapped = mapper.Map<UserResponse>(entity);
        return new ApiResponse<UserResponse>(mapped);
    }

    public async Task<ApiResponse<List<UserResponse>>> Handle(GetUserByParameterQuery request, CancellationToken cancellationToken)
    {
        var users = await unitOfWork.UserRepository.GetAllWithIncludeAsync(
            x => x.Email == request.FirstName,
            p => p.DigitalWallet,
            p => p.FirstName,
            p => p.LastName
        );
        var customer = users.FirstOrDefault();
        var mapped = mapper.Map<UserResponse>(customer);
        var mappedList = new List<UserResponse> { mapped };
        return new ApiResponse<List<UserResponse>>(mappedList);
    }
}