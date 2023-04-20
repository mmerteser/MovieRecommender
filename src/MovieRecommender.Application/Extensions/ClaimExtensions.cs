using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace MovieRecommender.Application.Extensions
{
    public static class ClaimExtensions
    {
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
        }

        public static void AddName(this ICollection<Claim> claims, string name)
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }

        public static void AddIdentifier(this ICollection<Claim> claims, string userId)
        {
            claims.Add(new Claim("userId", userId));
        }

        public static void AddMenus(this ICollection<Claim> claims, string[] menuIds)
        {
            menuIds.ToList().ForEach(menuId => claims.Add(new Claim(ClaimTypes.Role, menuId)));
        }
    }
}
