﻿using AutoMapper;
using DigiShop.Bussiness.Cqrs;
using DigiShop.Data.UnitOfWork;
using DigiShop.Schema;
using DigiSopAPI.Base.Response;
using MediatR;
using DigiShop.Bussiness.Token;

namespace DigiShop.Bussiness.Command;

public class AuthorizationCommandHandler : IRequestHandler<CreateAuthorizationTokenCommand, ApiResponse<AuthorizationResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private ITokenService tokenService;

    public AuthorizationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.tokenService = tokenService;
    }

    public async Task<ApiResponse<AuthorizationResponse>> Handle(CreateAuthorizationTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.FirstOrDefault(x => x.UserName == request.Request.UserName);
        if (user is null)
            return new ApiResponse<AuthorizationResponse>("Invalid user informations. Check your UserName or password. E1");

        if (user.Password != CreateMD5(request.Request.Password))
        {
            return new ApiResponse<AuthorizationResponse>("Invalid user informations. Check your UserName or password. E1");
        }

        if (user.Status != true)
            return new ApiResponse<AuthorizationResponse>("Invalid user informations. Check your UserName or password. E2");

        var token = await tokenService.GetToken(user);
        AuthorizationResponse response = new AuthorizationResponse()
        {
            ExpireTime = DateTime.Now.AddMinutes(5),
            AccessToken = token,
            UserName = user.UserName
        };

        return new ApiResponse<AuthorizationResponse>(response);
    }

    private string CreateMD5(string input)
    {
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes).ToLower();

        }
    }
}