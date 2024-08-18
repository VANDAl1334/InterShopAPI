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
using NuGet.Protocol;
using InterShopAPI.DTO;
using AutoMapper;

namespace InterShopAPI.Controllers
{
    /// <summary>
    /// Класс контроллера для аутентификации и регистрации пользователей в системе
    /// </summary>
    [ApiController]
    [Route("api/auth/")]
    public class AuthController : ControllerBase
    {
        private readonly InterShopContext _context;
        private readonly IMapper _mapper;

        public AuthController(InterShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// POST-метод для регистрации пользователя в системе
        /// </summary>
        /// <param name="user">Информация о пользователе</param>
        /// <returns>Возвращает код 201, а также информацию о пользователе в случае успешной регистрации</returns>
        // POST: api/Auth
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<User>> Register(UserDetailDTO userDTO)
        {
            User user = _mapper.Map<User>(userDTO);
            user.Password = byteArrayToString(Convert.FromBase64String(user.Password));            
            user.Role = _context.Roles.FirstOrDefault(u => u.Name == "User");
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Created();            
        }
        [HttpPost]
        [Route("LoginExists")]
        public async Task<ActionResult<User>> LoginExists(User user)
        {
            if (_context.Users.Any(u => u.Login == user.Login))
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

        [HttpGet]
        [Route("Authorize")]
        public ActionResult Authorize()
        {
            try
            {
                string TokenKey = Request.Headers["Authorization"];
                TokenKey = TokenKey.Replace("Bearer ", "");
                JwtSecurityTokenHandler tokenHandler = new();
                JwtSecurityToken tokenIsLogin = tokenHandler.ReadJwtToken(TokenKey);
                string login = tokenIsLogin.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name).Value;
                UserMinimalDTO userDTO = _mapper.Map<UserMinimalDTO>(_context.Users.FirstOrDefault(l => l.Login == login));
                var response = new
                {
                    userJson = userDTO
                };
                return Ok(response);
            }
            catch { return Conflict(); }

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
        [Route("Login")]
        public async Task<ActionResult> Login(UserDetailDTO? userDTO)
        {
            string passwordString = byteArrayToString(Convert.FromBase64String(userDTO.Password));
            User user = new();
            // Поикс пользователя в БД по логину и хэшу пароля
            user = await _context.Users.FirstOrDefaultAsync(x => x.Login == userDTO.Login && x.Password == passwordString);

            // Если пользователь не найден - возвращается код 404
            if (user == null)
            {
                return NotFound();
            }

            // Если пользователь найден - создаётся токен
            List<Claim> claims = new List<Claim>() { new Claim(ClaimTypes.Name, user.Login) };

            JwtSecurityToken token = new(
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
            };
            // HttpContext.Response.Cookies.Append("alalhToken", accessToken); //добавление token в Cookie        
            return AcceptedAtAction("Login", new { id = user.Id }, response);
        }
    }
}
