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
using InterShopAPI.Libs;

namespace InterShopAPI.Controllers
{
    /// <summary>
    /// Класс контроллера для аутентификации и регистрации пользователей в системе
    /// </summary>
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
        [Route("api/auth/Register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            user.Password = byteArrayToString(Convert.FromBase64String(user.Password));
            user.Role = _context.Roles.FirstOrDefault(u => u.Name == "User");
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var response = new
            {
                userData = user
            };

            return CreatedAtAction("Register", new { id = user.Id }, response);
        }
        [HttpPost]
        [Route("api/auth/LoginExists")]
        public async Task<ActionResult<User>> LoginExists(User user)
        {
            if (!_context.Users.Any(u => u.Login == user.Login))
                return Conflict();
            else
                return Ok();
        }

        /// <summary>
        /// Метод. Преобразует массив байт в строку
        /// </summary>
        /// <param name="array"><Массив байт/param>
        /// <returns>Строка</returns>/
        private string byteArrayToString(byte[] array)
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte num in array)
            {
                builder.Append(num);
            }

            return builder.ToString();
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
        [Route("api/auth/Login")]
        public async Task<ActionResult> Login(User? user)
        {
            string passwordString = byteArrayToString(Convert.FromBase64String(user.Password));
            // Поикс пользователя в БД по логину и хэшу пароля
            user = await _context.Users.FirstOrDefaultAsync(x => x.Login == user.Login && x.Password == passwordString);

            // Если пользователь не найден - возвращается код 404
            if (user == null)
            {
                return Conflict(user.Password);
            }

            // Если пользователь найден - создаётся токен
            List<Claim> claims = new List<Claim>() { new Claim(ClaimTypes.Name, user.Login) };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(2)),
                signingCredentials: new SigningCredentials(LibJWT.ExtendKeyLengthIfNeeded(AuthOptions.GetSymmetricSecurityKey(), 256), SecurityAlgorithms.HmacSha256)
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
