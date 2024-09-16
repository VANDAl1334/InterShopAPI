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
public class OrderController : ControllerBase
{
    private readonly InterShopContext _context;
    private readonly IMapper _mapper;

    public OrderController(InterShopContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult> GetOrders(int userId)
    {
        string tokenKey = Request.Headers["Authorization"];
        User? user = _context.Users.FirstOrDefault(u => u.Login == LibJWT.TokenIsLogin(tokenKey));

        // Если пользователь не найден или удалён
        if (user is null || user.IsDeleted == true)
        {
            return BadRequest(new ErrorMessage() { Code = -1, Message = "Пользователь не найден или удалён" });
        }

        // Если в параметре не указан 
        userId = userId == 0 ? user.Id : userId;

        // Если пользователь с ролью "Пользователь" пытается просмотреть заказы другого пользователя
        if (user.Id != userId && user.RoleId == 1)
        {
            return BadRequest();
        }

        IEnumerable<Order> orders = _context.Orders.Where(o => o.UserId == userId)
            .Include(p => p.OrderDetails).Include(p => p.OrderStatus)
            .Include(p => p.DeliveryType)
            .Include(p => p.PaymentType).Include(p => p.PayStatus);

        IEnumerable<OrderMinimalDTO> ordersDto = _mapper.Map<IEnumerable<OrderMinimalDTO>>(orders);

        return Ok(ordersDto);
    }

    [HttpGet("{orderId}")]
    [Authorize]
    public async Task<ActionResult<OrderDetailDTO>> GetOrder(int orderId)
    {
        string tokenKey = Request.Headers["Authorization"];
        User? user = _context.Users.FirstOrDefault(u => u.Login == LibJWT.TokenIsLogin(tokenKey));

        // Если пользователь не найден или удалён
        if (user is null || user.IsDeleted)
        {
            return BadRequest(new ErrorMessage() { Code = -1, Message = "Пользователь не найден или удалён" });
        }

        Order? order = await _context.Orders
            .Include(p => p.OrderDetails).ThenInclude(od => od.ProductVariant).ThenInclude(pv => pv.PriceHistories)
            .Include(p => p.OrderStatus)
            .Include(p => p.DeliveryType)
            .Include(p => p.PaymentType)
            .Include(p => p.PayStatus).FirstOrDefaultAsync(o => o.Id == orderId);

        if (order is null || (user.RoleId == 1 && order.UserId != user.Id))
        {
            return BadRequest(new ErrorMessage() { Code = -1, Message = "Информация о заказе не доступна" });
        }

        return Ok(_mapper.Map<OrderDetailDTO>(order));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> PostOrder(OrderDetailDTO orderDto)
    {
        string tokenKey = Request.Headers["Authorization"];
        User? user = _context.Users.FirstOrDefault(u => u.Login == LibJWT.TokenIsLogin(tokenKey));

        // Если пользователь не найден или удалён
        if (user is null || user.IsDeleted)
        {
            return BadRequest(new ErrorMessage() { Code = -1, Message = "Пользователь не найден или удалён" });
        }

        orderDto.UserId = user.Id;

        Order order = _mapper.Map<Order>(orderDto);
        order.OrderStatusId = _context.OrderStatuses.FirstOrDefault(os => os.Name == "В пути").Id;
        order.DeliveryTypeId = _context.DeliveryTypes.FirstOrDefault(os => os.Name == orderDto.DeliveryType).Id;
        order.PaymentTypeId = _context.PaymentTypes.FirstOrDefault(os => os.Name == orderDto.PaymentType).Id;
        order.PayStatusId = _context.PayStatuses.FirstOrDefault(os => os.Name == "Не оплачен").Id;

        order.OrderStatus = null;
        order.PayStatus = null;

        foreach(OrderDetails detail in order.OrderDetails)
        {
            detail.ProductVariant = null;
        }
    
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return Ok("Дилдо");
    }
}