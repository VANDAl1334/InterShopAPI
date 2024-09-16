using AutoMapper;
using InterShopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace InterShopAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeliveryTypeController : ControllerBase
{
    private readonly InterShopContext _context;
    private readonly IMapper _mapper;

    public DeliveryTypeController(InterShopContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DeliveryType>>> GetDeliveryTypes()
    {
        return Ok(_context.DeliveryTypes);
    }
}