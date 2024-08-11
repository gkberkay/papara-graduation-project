using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace DigiShop.Base.Helpers
{
    public static class HelperMethods
    {
        public static string GetClaimInfo(string type)
        {
            var httpContext = new HttpContextAccessor().HttpContext;

            if (httpContext == null)
            {
                return null;
            }

            var request = httpContext.Request;

            var tokenString = request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (tokenString.IsNullOrEmpty()) return null;

            var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);

            if (token == null || token.Claims == null)
            {
                return null;
            }

            return token.Claims.FirstOrDefault(x => x.Type == type)?.Value?.ToString();
        }

        public static string GenerateRandomText(int length = 9)
        {
            const string chars = "0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}