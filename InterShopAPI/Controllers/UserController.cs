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

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDetailDTO>>> GetUsers(string roleName)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Login == LibJWT.TokenIsLogin(Request.Headers["Authorization"]));
            if (user.RoleId == 1 || user.IsDeleted)
                return BadRequest();
            bool roleExists = _context.Roles.Any(x => x.Name == roleName);
            if (!roleExists)
                return BadRequest();
            return Ok(_mapper.Map<IEnumerable<UserDetailDTO>>(_context.Users.Where(p => p.Role.Name == roleName).Include(p => p.Role)));
        }

        // GET: api/User/5        
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDetailDTO>> GetUser(int id)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Login == LibJWT.TokenIsLogin(Request.Headers["Authorization"]));
            if (user.RoleId == 1 || user.IsDeleted)
                return BadRequest();
            UserDetailDTO userModel = _mapper.Map<UserDetailDTO>(await _context.Users.Where(u => u.Id == id).Include(u => u.Role).FirstOrDefaultAsync());
            if (userModel == null || userModel.IsDeleted == true)
                return NotFound();
            return Ok(userModel);
        }

        // PUT: api/Auth/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> PatchUser(string loginDTO, UserDetailDTO userDTO)
        {
            // Получение пользователя из токена (редактора) 
            User editorIsToken = await _context.Users.Where(x => x.Login == LibJWT.TokenIsLogin(Request.Headers["Authorization"])).AsNoTracking().FirstOrDefaultAsync();
            User userModel = await _context.Users.FirstOrDefaultAsync(u => u.Login == loginDTO);
            User userClient = _mapper.Map<User>(userDTO);
            // Проверка на то, что редактор не пытается изменить неизменяемые поля:
            // логин, роль, подтверждённость почты
            if (editorIsToken == null ||
                userDTO.Login != null ||
                userDTO.RoleName != null ||
                userDTO.InstanseMail != null ||
                userDTO.IsDeleted != null ||
                userModel.IsDeleted)
                return BadRequest();
            // Получение пользователя из PATCH-запроса
            // userModel.RoleId = _context.Roles.FirstOrDefault(u => u.Name == userDTO.RoleName).Id;
            // Проверка на роль. Обычный пользователь не может изменять пароль
            // Проверка на то что редактор не пытается изменить другого пользователя
            // редактировать другого пользователя может только администратор
            if (editorIsToken.Id != userModel.Id && editorIsToken.RoleId != 3)
                return BadRequest();
            if (userClient.Mail != null)
                userModel.Mail = userClient.Mail;
            // if(userClient.Password != null && userModel.instanseMail)
            //    userModel.Password = Будущий метод Принимающий(userClient.Password);///////////////////////
            _context.Entry(userModel).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync(); //Требуется обработка 500 ошибки
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userModel.Id))
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
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteUser(string loginDTO, bool isDeleting)
        {
            User editorIsToken = await _context.Users.Where(x => x.Login == LibJWT.TokenIsLogin(Request.Headers["Authorization"])).AsNoTracking().FirstOrDefaultAsync();
            var userModel = await _context.Users.FirstOrDefaultAsync(u => u.Login == loginDTO);
            if (userModel == null || userModel.IsDeleted)
                return NotFound();
            if (editorIsToken.RoleId == 1)
                if (editorIsToken.Id == userModel.Id && isDeleting == true)
                    userModel.IsDeleted = isDeleting;
                else
                    return BadRequest();
            else if (editorIsToken.RoleId == 2)
            {
                if (userModel.RoleId == 1)
                    userModel.IsDeleted = isDeleting;
                else
                    return BadRequest();
            }
            else if (editorIsToken.RoleId == 3)
                if (userModel.RoleId != 3 || userModel.Id == editorIsToken.Id)
                    userModel.IsDeleted = isDeleting;
                else
                    return BadRequest();
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
