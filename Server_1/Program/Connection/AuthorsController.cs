using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;    

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly AppDbContext _context;
    public AuthorsController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<IEnumerable<Author>> GetAuthors() =>
        await _context.Authors.OrderBy(a => a.Name).ToListAsync();
}