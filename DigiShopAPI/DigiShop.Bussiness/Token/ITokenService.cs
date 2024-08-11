using DigiShop.Data.Domain;

namespace DigiShop.Bussiness.Token;

public interface ITokenService
{
    Task<string> GetToken(string username);
    Task<string> GetToken(int userId);
    Task<string> GetToken(User user);
}
