using AutoMapper;
using InterShopAPI.DTO;
using InterShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterShopAPI;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly InterShopContext _context;
    private readonly IMapper _mapper;

    public CommentController(InterShopContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize]
    public IActionResult PostComment(CommentDTO commentDto)
    {
        User user = _context.Users.First(p => p.Login == commentDto.Login);
        Product product = _context.Products.First(p => p.Id == commentDto.ProductId);

        Comment comment = _mapper.Map<Comment>(commentDto);
        comment.User = user;
        comment.UserId = user.Id;
        comment.DateTime = DateTime.UtcNow;

        if(_context.Comments.Any(p => p.User == user && p.Product == product))
        {
            return BadRequest("Вы уже оставляли отзыв на этот товар");
        }

        try
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            return BadRequest();
        }

        return Created();
    }
}