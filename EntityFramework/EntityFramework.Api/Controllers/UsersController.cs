using EntityFramework.Api.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController(AppDbContext dbContext) : Controller
{
    /// <summary>
    /// Retrieves a user with ID. 
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns>The requested user.</returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetById([FromRoute] Guid id)
    {
        //Usage of AsSplitQuery() will significantly improve performance
        //since we will have multiple smaller queries instead of one giant query
        var user = await dbContext
            .Users
            .Include(u => u.Boards)
            .ThenInclude(b => b.Columns)
            .ThenInclude(c => c.Cards)
            .AsSplitQuery()
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user is null)
        {
            return NotFound($"Could not find user with id: {id}");
        }
        
        return Ok(user);
    }
}