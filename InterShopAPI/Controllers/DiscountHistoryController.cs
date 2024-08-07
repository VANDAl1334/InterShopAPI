using InterShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace InterShopAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiscountHistoryController : Controller
{
    private readonly InterShopContext _context;

    public DiscountHistoryController(InterShopContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Метод валидации. Проверяет значение поля DateFrom
    /// </summary>
    /// <returns></returns>
    [AcceptVerbs("Get", "Post")]
    public IActionResult ValidateDateFrom(DateOnly dateFrom)
    {
        if(dateFrom < DateOnly.Parse("01-01-2020"))
            return Json(false); 
        return Json(true);
    }

    /// <summary>
    /// Метод валидации. Проверяет значение поля DateFrom
    /// </summary>
    /// <returns></returns>
    [AcceptVerbs("Get", "Post")]
    public IActionResult ValidateDateTo(DateOnly dateTo)
    {
        if(dateTo < DateOnly.Parse("01-01-2020"))
            return Json(false); 
        return Json(true);
    }
}