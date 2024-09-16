using AutoMapper;
using InterShopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace InterShopAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentTypeController : ControllerBase
{
    private readonly InterShopContext _context;
    private readonly IMapper _mapper;

    public PaymentTypeController(InterShopContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaymentType>>> GetPaymentTypes()
    {
        return Ok(_context.PaymentTypes);
    }
}