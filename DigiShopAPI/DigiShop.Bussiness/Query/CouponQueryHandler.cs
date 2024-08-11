using AutoMapper;
using DigiShop.Data.UnitOfWork;
using DigiSopAPI.Base.Response;
using MediatR;
using DigiShop.Bussiness.Cqrs;
using DigiShop.Data.Domain;
using DigiShop.Schema;

namespace DigiShop.Bussiness.Query;

public class CouponQueryHandler :
    IRequestHandler<GetAllCouponQuery, ApiResponse<List<CouponResponse>>>,
    IRequestHandler<GetCouponByCodeQuery, ApiResponse<CouponResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CouponQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<CouponResponse>>> Handle(GetAllCouponQuery request, CancellationToken cancellationToken)
    {
        List<Coupon> entityList = await unitOfWork.CouponRepository.GetAll();
        var mappedList = mapper.Map<List<CouponResponse>>(entityList);
        return new ApiResponse<List<CouponResponse>>(mappedList);
    }

    public async Task<ApiResponse<CouponResponse>> Handle(GetCouponByCodeQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.CouponRepository.FirstOrDefaultAsNoTracking(x => x.Code == request.Code);
        var mapped = mapper.Map<CouponResponse>(entity);
        return new ApiResponse<CouponResponse>(mapped);
    }
}