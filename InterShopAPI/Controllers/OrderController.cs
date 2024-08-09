using InterShopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace InterShopAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly InterShopContext _context;

    public OrderController(InterShopContext context)
    {
        _context = context;
    }


    /// <summary>
    /// Метод валидации. Проверяет значение поля DateTime
    /// </summary>
    /// <param name="dateTime">Дата и время заказа</param>
    /// <returns></returns>
    // public bool ValidateDateTime(DateTime dateTime)
    // {
    //     if(dateTime < DateTime.Parse("01-01-2020"))
    //         return false;
    //     return true;
    // }
}