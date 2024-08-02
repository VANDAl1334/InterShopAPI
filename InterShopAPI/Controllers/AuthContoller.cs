using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using InterShopAPI.Models;

namespace InterShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly InterShopContext _context;

        public AuthController(InterShopContext context)
        {
            _context = context;
        }

        /// <summary>
        /// POST-метод для регистрации пользователя в системе
        /// </summary>
        /// <param name="user">Информация о пользователе</param>
        /// <returns>Возвращает код 201, а также информацию о пользователе в случае успешной регистрации</returns>
        // POST: api/Auth
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("api/[controller]/Register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var response = new
            {
                userData = user
            };

            return CreatedAtAction("Register", new { id = user.Id }, response);
        }


        /// <summary>
        /// POST-метод для аутентификации пользователя по логину и паролю
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Хэш пароля</param>
        /// <returns>Возвращает код 201, а также информацию о пользователе и хэш токена в случае успешной авторизации</returns>
        // POST: api/Auth
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("api/[controller]/Login")]
        public async Task<ActionResult<User>> Login(string login, string password)
        {
            // Поикс пользователя в БД по логину и хэшу пароля
            User? user = await _context.Users.FirstOrDefaultAsync(x => x.Login == login && x.Password == password);

            // Если пользователь не найден - возвращается код 404
            if (user == null)
            {
                return NotFound();
            }

            // Если пользователь найден - создаётся токен
            List<Claim> claims = new List<Claim>() { new Claim(ClaimTypes.Name, user.Login) };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(2)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            // Запись токена и получение хэша токена
            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            // Передача хэша токена и информации о пользователе в ответ
            var response = new
            {
                accessToken = encodedJwt,
                userData = user
            };

            return AcceptedAtAction("Login", new { id = user.Id }, response);
        }
    }
}
