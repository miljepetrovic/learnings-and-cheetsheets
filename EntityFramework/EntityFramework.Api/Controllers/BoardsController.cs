using EntityFramework.Api.Database;
using EntityFramework.Api.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BoardsController(AppDbContext dbContext) : ControllerBase
{
    /// <summary>
    /// Retrieves boards by user id.
    /// </summary>
    /// <param name="userId">The id of the board's owner.</param>
    /// <returns>Requested list of the boards</returns>
    [HttpGet]
    public async Task<ActionResult> GetBoardsByUserId([FromQuery] Guid userId)
    {
        //This endpoint is used for displaying list of the boards in the sidebar on the FE. 
        //Good use case for *projection*, since we will retrieve only data that we need. 
        var boards = await dbContext
            .Boards
            .AsNoTracking()
            .Where(b => b.UserId == userId && b.Status == Status.Active)
            .Select(b => new { b.Id, b.Name }) //*Projection*
            .ToListAsync();
        
        return Ok(boards);
    }

    /// <summary>
    /// Archives board by ID.
    /// </summary>
    /// <param name="id">The ID of the board.</param>
    /// <returns>No Content</returns>
    [HttpPut("id:guid")]
    public async Task<ActionResult> Archive([FromRoute] Guid id)
    {
        var board = await dbContext
            .Boards
            .Include(b => b.Columns)
            .ThenInclude(c => c.Cards)
            .AsSplitQuery()
            .FirstOrDefaultAsync(b => b.Id == id);

        if (board is null)
        {
            return NotFound($"Could not find board with id: {id}.");
        }

        //Archive board and all columns and cards.
        board.Archive();
        foreach (var column in board.Columns)
        {
            column.Archive();
            foreach (var card in column.Cards)
            {
                card.Archive();
            }
        }
        
        //By calling SaveChangesAsync() once after all updates, we reduce the number of trips to the database.
        await dbContext.SaveChangesAsync();
        
        return NoContent();
    }
}