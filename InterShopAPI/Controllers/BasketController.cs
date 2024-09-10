using AutoMapper;
using InterShopAPI.DTO;
using InterShopAPI.Libs;
using InterShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterShopAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketController : ControllerBase
{
    private readonly InterShopContext _context;
    private readonly IMapper _mapper;

    public BasketController(InterShopContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<BasketDTO>> GetBasket()
    {
        string tokenKey = Request.Headers["Authorization"];
        User? user = _context.Users.FirstOrDefault(u => u.Login == LibJWT.TokenIsLogin(tokenKey));

        // Если пользователь не найден или удалён или его роль не "Пользователь"
        if (user is null || user.IsDeleted == true || user.RoleId != 1)
        {
            return BadRequest();
        }

        List<Basket> basket = _context.Baskets.Where(b => b.UserId == user.Id)
            .Include(b => b.ProductVariant)
                .ThenInclude(p => p.PriceHistories)
            .Include(b => b.ProductVariant)
                .ThenInclude(p => p.Product).ThenInclude(p => p.DiscountHistories).ToList();

        List<BasketDTO> basketDTO = _mapper.Map<List<BasketDTO>>(basket);

        return Ok(basketDTO);
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult> PutBasket(IEnumerable<BasketDTO> basketDTO)
    {
        string tokenKey = Request.Headers["Authorization"];
        User? user = _context.Users.FirstOrDefault(u => u.Login == LibJWT.TokenIsLogin(tokenKey));

        // Если пользователь не найден или удалён или его роль не "Пользователь"
        if (user is null || user.IsDeleted == true || user.RoleId != 1)
        {
            return BadRequest();
        }

        IEnumerable<Basket> basket = _mapper.Map<IEnumerable<Basket>>(basketDTO);

        return Ok();
    }

    [HttpPatch]
    [Authorize]
    public async Task<ActionResult<BasketDTO>> PatchBasket(int productVariantId, int count)
    {
        string tokenKey = Request.Headers["Authorization"];
        User? user = _context.Users.FirstOrDefault(u => u.Login == LibJWT.TokenIsLogin(tokenKey));

        // Если пользователь не найден или удалён или его роль не "Пользователь"
        if (user is null || user.IsDeleted == true || user.RoleId != 1)
        {
            return BadRequest();
        }

        Basket basketToUpdate = await _context.Baskets.FirstOrDefaultAsync(b => b.UserId == user.Id && b.ProductVariantId == productVariantId);

        if (basketToUpdate is not null)
        {
            basketToUpdate.Count += count;
            _context.Baskets.Update(basketToUpdate);
        }
        else
        {
            basketToUpdate = new Basket() { UserId = user.Id, ProductVariantId = productVariantId, Count = count };
            _context.Baskets.Add(basketToUpdate);
        }

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete]
    [Authorize]
    public async Task<ActionResult> DeleteBasket(int productVariantId)
    {
        string tokenKey = Request.Headers["Authorization"];
        User? user = await _context.Users.FirstOrDefaultAsync(u => u.Login == LibJWT.TokenIsLogin(tokenKey));

        // Если пользователь не найден или удалён или его роль не "Пользователь"
        if (user is null || user.IsDeleted == true || user.RoleId != 1)
        {
            return BadRequest();
        }

        Basket basketToDelete = await _context.Baskets.FirstOrDefaultAsync(b => b.UserId == user.Id && b.ProductVariantId == productVariantId);

        if (basketToDelete is null)
        {
            return BadRequest("ProductVariant don't exists in basket");
        }

        _context.Baskets.Remove(basketToDelete);
        await _context.SaveChangesAsync();

        return Ok();
    }
}