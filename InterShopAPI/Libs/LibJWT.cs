using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using InterShopAPI.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
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
        // public static bool CheckToken(string token, InterShopContext context)
        // {
        //     User? user = context.Users.FirstOrDefault(u => u.JwtToken == token);
        //     if (user == null)
        //         return false;            
        //     return true;
        // }
        public static string TokenIsLogin(string TokenKey)
        {            
            TokenKey = TokenKey.Replace("Bearer ", "");
            if (TokenKey == "undefined" || TokenKey == null)
                return null;
            JwtSecurityTokenHandler tokenHandler = new();
            JwtSecurityToken tokenIsLogin = tokenHandler.ReadJwtToken(TokenKey);
            string login = tokenIsLogin.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name).Value;
            return login;
        }
        public static string AppendSalt(string password)
        {
            string saltPassword = password + "S0m3Stat1cSalt!";
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltPassword));
                StringBuilder builder = new();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}