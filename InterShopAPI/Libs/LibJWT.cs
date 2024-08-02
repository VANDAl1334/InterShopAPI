using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using InterShopAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace InterShopAPI.Libs
{
    public static class LibJWT
    {
        public static string CreateToken(User user)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Login) };
            // создаём JWT-токкен
            var jwt = new JwtSecurityToken(
                issuer: "InterShop",
                audience: user.Login,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(ExtendKeyLengthIfNeeded(AuthOptions.GetSymmetricSecurityKey(), 256), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        public static SymmetricSecurityKey ExtendKeyLengthIfNeeded(SymmetricSecurityKey key, int minLenInBytes)
        {
            if (key != null && key.KeySize < (minLenInBytes * 8))
            {
                var newKey = new byte[minLenInBytes]; // zeros by default
                key.Key.CopyTo(newKey, 0);
                return new SymmetricSecurityKey(newKey);
            }
            return key;
        }
        public static bool CheckToken(string token, InterShopContext context)
        {
            User? user = context.Users.FirstOrDefault(u => u.JwtToken == token);
            if (user == null)
                return false;            
            return true;
        }
    }
}