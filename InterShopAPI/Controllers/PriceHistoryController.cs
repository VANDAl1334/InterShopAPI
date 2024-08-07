using InterShopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace InterShopAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PriceHistoryController : ControllerBase
{
    private readonly InterShopContext _context;

    public PriceHistoryController(InterShopContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Метод валидации. Проверяет значение поля Date
    /// </summary>
    /// <param name="date">Дата изменения цены</param>
    /// <returns></returns>
    public bool ValidateDate(DateOnly date)
    {
        if (date < DateOnly.Parse("01-01-2020"))
            return false;
        return true;
    }
}