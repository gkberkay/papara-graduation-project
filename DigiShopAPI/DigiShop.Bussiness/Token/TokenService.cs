using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DigiShop.Base.Token;
using DigiShop.Data.Domain;
using DigiShop.Data.UnitOfWork;
using Microsoft.IdentityModel.Tokens;

namespace DigiShop.Bussiness.Token;

public class TokenService : ITokenService
{
    private readonly JwtConfig jwtConfig;
    private readonly IUnitOfWork unitOfWork;

    public TokenService(JwtConfig jwtConfig, IUnitOfWork unitOfWork)
    {
        this.jwtConfig = jwtConfig;
        this.unitOfWork = unitOfWork;
    }

    public async Task<string> GetToken(string username)
    {
        User user = await unitOfWork.UserRepository.FirstOrDefaultAsNoTracking(x => x.UserName == username);

        return await GetToken(user);
    }

    public async Task<string> GetToken(int userId)
    {
        User user = await unitOfWork.UserRepository.FirstOrDefaultAsNoTracking(x => x.Id == userId);
        return await GetToken(user);
    }

    public async Task<string> GetToken(User user)
    {
        Claim[] claims = GetClaims(user);
        var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

        JwtSecurityToken jwtToken = new JwtSecurityToken(
            jwtConfig.Issuer,
            jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddDays(jwtConfig.AccessTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret),
                SecurityAlgorithms.HmacSha256Signature)
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return token;
    }

    private Claim[] GetClaims(User user)
    {
        var claims = new[]
        {
            new Claim("UserName", user.UserName),
            new Claim("Id", user.Id.ToString()),
            new Claim("Role", user.Role),
            new Claim("Status", user.Status.ToString()),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
        };
        return claims;
    }
}