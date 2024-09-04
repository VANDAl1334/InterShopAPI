using System.ComponentModel;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using AutoMapper;
using InterShopAPI.DTO;
using InterShopAPI.Libs;
using InterShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace InterShopAPI.Controllers;

[Authorize]
[Route("api/favourite/")]
public class FavouriteController : ControllerBase
{
    private readonly InterShopContext _context;
    private readonly IMapper _mapper;

    public FavouriteController(InterShopContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetFavoriteProducts(bool productInfo)
    {
        // Получаем пользователя по логину из хеша токена
        User? user = _context.Users.Include(u => u.FavouriteProducts).ThenInclude(fp => fp.Product)
            .FirstOrDefault(u => u.Login == LibJWT.TokenIsLogin(Request.Headers["Authorization"]));

        // Если пользователь не найден
        if (user == null)
            return BadRequest();
        // Если у пользователя не роль "Пользователь"
        if (user.RoleId != 1)
            return BadRequest();

        // Получаем список избранных товаров
        FavouriteProduct[] favouriteProducts = user.FavouriteProducts.ToArray();
        int[] productsId = new int[user.FavouriteProducts.Count];

        for (int i = 0; i < favouriteProducts.Length; i++)
        {
            productsId[i] = favouriteProducts[i].ProductId;
        }

        if (!productInfo)
            return Ok(productsId);

        // Получаем информацию о товарах
        List<Product> products = _context.Products
            .Where(p => productsId.Contains(p.Id))
                    .Include(p => p.ProductVariants.Where(p => p.IsMain))
                        .ThenInclude(p => p.PriceHistories)
                    .Include(p => p.Category)
                    .Include(p => p.Comments)
                    .Include(p => p.DiscountHistories)
                .ToList();

        return Ok(_mapper.Map<List<ProductMinimalDTO>>(products));
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> PutFavourite(int[] productsId)
    {
        using(StreamReader streamReader = new StreamReader(Request.Body, Encoding.UTF8))
        {
            string jsonBody = await streamReader.ReadToEndAsync();
            productsId = JsonConvert.DeserializeObject<int[]>(jsonBody);
        }
        
        // Получаем пользователя по логину из хеша токена
        User? user = _context.Users.Include(u => u.FavouriteProducts).FirstOrDefault(u => u.Login == LibJWT.TokenIsLogin(Request.Headers["Authorization"]));
        // Если пользователь не найден
        if (user == null)
            return BadRequest();
        // Если у пользователя не роль "Пользователь"
        if (user.RoleId != 1)
            return BadRequest();

        user.FavouriteProducts.Clear();
        foreach (int productId in productsId)
        {
            user.FavouriteProducts.Add(new FavouriteProduct() { UserId = user.Id, ProductId = productId });
        }
        
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return Ok();
    }
}