using AutoMapper;
using InterShopAPI.DTO;
using InterShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace InterShopAPI.Controllers;


[ApiController]
[Route("api/{controller}")]
public class ProductController : ControllerBase
{
    private readonly InterShopContext _context;
    private readonly IMapper _mapper;

    public ProductController(InterShopContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet()]
    public IActionResult GetProducts(int? categoryId, bool deleted, bool notSales, bool discountOnly, bool variants, bool allImages, string? nameFilter)
    {
        if (categoryId == null)
            categoryId = 0;

        if (nameFilter == null)
            nameFilter = string.Empty;

        IEnumerable<Product> products = _context.Products
            .Where(p => (p.CategoryID == categoryId || (categoryId == 0))
                && (p.Name.ToLower().Contains(nameFilter.ToLower()) || (nameFilter == string.Empty))
                && (p.IsDeleted == false || deleted)
                && (p.OnSale == true || notSales))
                .Include(p => p.ProductVariants.Where(p => p.IsMain || variants))
                    .ThenInclude(p => p.ProductVariantCharacteristics).ThenInclude(p => p.Characteristic)
                .Include(p => p.ProductVariants.Where(p => p.IsMain || variants))
                    .ThenInclude(p => p.PriceHistories)
                .Include(p => p.ImagesOfProduct.Where(p => allImages))
                .Include(p => p.Category)
                .Include(p => p.DiscountHistories)
            .Where(p => p.DiscountHistories
                .Any(d => d.DateFrom <= DateOnly.FromDateTime(DateTime.Now) && d.DateTo >= DateOnly.FromDateTime(DateTime.Now)) || !discountOnly);

        IEnumerable<ProductDTO> productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);

        return Ok(productsDto);
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        Product? product = _context.Products
            .Where(p => p.Id == id)
            .Include(p => p.ProductVariants)
                    .ThenInclude(p => p.ProductVariantCharacteristics).ThenInclude(p => p.Characteristic)
            .Include(p => p.ProductVariants)
                .ThenInclude(p => p.PriceHistories)
            .Include(p => p.ImagesOfProduct)
            .Include(p => p.Category)
            .Include(p => p.DiscountHistories)
            .Include(p => p.Comments)
                .ThenInclude(p => p.User)
            .FirstOrDefault();

        ProductDTO productDto = _mapper.Map<ProductDTO>(product);

        return Ok(productDto);
    }
}