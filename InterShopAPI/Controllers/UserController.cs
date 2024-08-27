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
using InterShopAPI.DTO;
using NuGet.Configuration;
using AutoMapper;
using InterShopAPI.Libs;

namespace InterShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly InterShopContext _context;
        private readonly IMapper _mapper;

        public UserController(InterShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/User
        //[Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<UserDetailDTO>> GetUsers(string roleName)
        {
            bool roleExists = _context.Roles.Any(x => x.Name == roleName);
            if (!roleExists)
                return BadRequest();
            return Ok(_mapper.Map<IEnumerable<UserDetailDTO>>(_context.Users.Where(p => p.Role.Name == roleName).Include(p => p.Role)));
        }

        // GET: api/User/5
        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailDTO>> GetUser(int id)
        {
            UserDetailDTO user = _mapper.Map<UserDetailDTO>(await _context.Users.Where(u => u.Id == id).Include(u => u.Role).FirstOrDefaultAsync());
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // PUT: api/Auth/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUser(int id, UserDetailDTO userDTO)
        {
            string TokenKey = Request.Headers["Authorization"];
            string login = LibJWT.TokenIsLogin(TokenKey);

            // Получение пользователя из токена (редактора) 
            User editorToken = _context.Users.Where(x => x.Login == login).AsNoTracking().FirstOrDefault();
            
            // Проверка на то, что редактор не пытается изменить неизменяемые поля:
            // логин, роль, подтверждённость почты
            if (editorToken == null ||
                userDTO.Login != null ||
                userDTO.RoleName != null ||
                userDTO.InstanseMail != null ||
                // условие на то, что обычный пользователь пытается изменить isDeleted
                (editorToken.RoleId == 1 && userDTO.IsDeleted != null))
                return BadRequest();

            // Получение пользователя из PUT-запроса
            User user = _context.Users.FirstOrDefault(u => u.Id == id);
            // User user = _mapper.Map<User>(userDTO);
            // user.RoleId = _context.Roles.FirstOrDefault(u => u.Name == userDTO.RoleName).Id;

            // Проверка на роль. Обычный пользователь не может изменять пароль
            if (editorToken?.RoleId == 1)
                user.Password = editorToken.Password;

            // Проверка на то что редактор не пытается изменить другого пользователя
            // редактировать другого пользователя может только администратор
            if (editorToken.Id != user.Id && editorToken?.RoleId != 3)
                return BadRequest();
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(); //обработка 500 ошибки
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // DELETE: api/Auth/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
