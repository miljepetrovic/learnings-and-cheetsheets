using EntityFramework.Api.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Api.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class CardsController(AppDbContext dbContext) : ControllerBase
{
    /// <summary>
    /// Search for cards by name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string name)
    {
        //Usage of the AsNoTracking, Projection and Index on the name for the better performance.
        var cards = await dbContext
            .Cards
            .AsNoTracking()
            .Where(c => EF.Functions.Like(c.Name, $"%{name}%"))
            .Select(c => new { c.Id, c.Name })
            .ToListAsync();
        
        return Ok(cards);
    }
}