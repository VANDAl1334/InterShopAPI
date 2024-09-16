using AutoMapper;
using InterShopAPI.DTO;
using InterShopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace InterShopAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShopController : ControllerBase
{
    private readonly InterShopContext _context;
    private readonly IMapper _mapper;

    public ShopController(InterShopContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShopDTO>>> GetShops()
    {
        IEnumerable<Shop> shops = _context.Shops;
        return Ok(_mapper.Map<IEnumerable<ShopDTO>>(shops));
    }
}