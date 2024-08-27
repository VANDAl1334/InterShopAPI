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
using System.Security.Cryptography;

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
            user.Password = LibJWT.AppendSalt(user.Password);
            user.Role = _context.Roles.FirstOrDefault(u => u.Name == "User");
            user.InstanseMail = false;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Created();
        }
        [HttpGet]
        [Route("LoginExists")]
        public async Task<ActionResult<User>> LoginExists(string login)
        {
            if (_context.Users.Any(u => u.Login == login))
                return Conflict();
            else
                return Ok();
        }

        /// <summary>
        /// Метод. Преобразует массив байт в строку
        /// </summary>
        /// <param name="array"><Массив байт/param>
        /// <returns>Строка</returns>/
        // private string byteArrayToString(byte[] array)
        // {
        //     StringBuilder builder = new StringBuilder();
        //     foreach (byte num in array)
        //     {
        //         builder.Append(num);
        //     }

        //     return builder.ToString();
        // }

        [HttpGet]
        [Route("Authorize")]
        public ActionResult Authorize()
        {
            try
            {
                string TokenKey = Request.Headers["Authorization"];
                UserMinimalDTO userDTO = _mapper.Map<UserMinimalDTO>(_context.Users.FirstOrDefault(l => l.Login == LibJWT.TokenIsLogin(TokenKey)));
                var response = new { userJson = userDTO };
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
            User user = new();
            // Поикс пользователя в БД по логину и хэшу пароля
            user = await _context.Users.FirstOrDefaultAsync(x => x.Login == userDTO.Login && x.Password == LibJWT.AppendSalt(userDTO.Password));

            // Если пользователь не найден - возвращается код 404
            if (user == null)
                return NotFound();

            // Если пользователь найден - создаётся токен
            List<Claim> claims = new List<Claim>() { new(ClaimTypes.Name, user.Login) };

            JwtSecurityToken token = new(
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
                accessToken = encodedJwt
            };
            // HttpContext.Response.Cookies.Append("alalhToken", accessToken); //добавление token в Cookie        
            return Accepted(response);
        }
    }
}
