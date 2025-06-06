using Microsoft.AspNetCore.Mvc;
using RestApiBestPractices.Exceptions;
using RestApiBestPractices.Models;
using RestApiBestPractices.Services.Interfaces;

namespace RestApiBestPractices.Controllers;

[ApiController]
[Route("/v1/[controller]")] //Add API versioning
//Plural resources nouns. (Name controller in plural and by using [controller] in [Route] attribute.)
public class TicketsController(ITicketService ticketService) : ControllerBase
{
    /// <summary>
    /// Returns all tickets.
    /// </summary>
    /// <returns>List of tickets.</returns>
    /// <param name="page">Optional page number.</param>
    /// <param name="pageSize">Optional page size.</param>
    /// <response code="200">Returns a list of tickets.</response>
    /// <response code="400">Returns Bad Request (400) if one of the query parameters is invalid.</response>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TicketResponseDto>>> GetAll(
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 10
    ) //Implement pagination with optional query parameters
    {
        //Return Bad Request (400) status code if request parameters are invalid.
        if (page <= 0)
        {
            return BadRequest($"Parameter {nameof(page)} must be positive integers.");
        }
        if (pageSize <= 0 )
        {
            return BadRequest($"Parameter {nameof(pageSize)} must be positive integers.");
        }
        
        var tickets = await ticketService.GetAll(page, pageSize);
        //Returns the 200 (OK) status code with list of the tickets.
        return Ok(tickets);
    }
    
    /// <summary>
    /// Returns a ticket.
    /// </summary>
    /// <param name="id">The ID of the requested ticket.</param>
    /// <returns>Ticket object.</returns>
    /// <response code="200">Returns the ticket.</response>
    /// <response code="404">Ticket not found.</response>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TicketResponseDto>> GetById([FromRoute] Guid id) //[FromRoute] attribute for resource id, name
    {
        try
        {
            var ticket = await ticketService.GetById(id);
            return Ok(ticket); // If ticket is found, returns 200 (OK) status code with ticket object.
        }
        catch (TicketNotFoundException)
        {
            //Returns the 404 (Not Found) status code if ticket is not found. (Services layer should handle check.)
            return NotFound($"Could not find ticket with id: {id}.");
        }
    }
    
    /// <summary>
    /// Create a new ticket.
    /// </summary>
    /// <param name="request">Ticket request data.</param>
    /// <returns>Newly created ticket.</returns>
    /// <response code="201">Ticket created.</response>
    /// <response code="400">Invalid input.</response>
    [HttpPost]
    public async Task<ActionResult<TicketResponseDto>> Create([FromBody] TicketRequestDto request) //[FromRoute] Bind request to explicit type.
    {
        var createdTicket = await ticketService.Create(request);
        // Returns 201 (Created) status code with Location header pointing to GET /api/tickets/{id}
        return CreatedAtAction(
            nameof(GetById),
            new { id = createdTicket.Id },
            createdTicket
        );
    }

    /// <summary>
    /// Update an existing ticket.
    /// </summary>
    /// <param name="id">The ID of the ticket that is being updated.</param>
    /// <param name="request">Updated ticket data.</param>
    /// <response code="204">Ticket updated.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="404">Ticket not found.</response>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id, 
        [FromBody] TicketRequestDto request
    ) // In this example, we can use IActionResult since we will return status code without content.
    {
        try
        {
            await ticketService.Update(id, request);
            //Returns 204 (No Content) status code if ticket is updated successfully. 
            return NoContent();
        }
        catch (TicketNotFoundException)
        {
            //Returns 404 (Not Found) status code if ticket is not found.
            return NotFound($"Could not find ticket with id: {id}.");
        }
    }
    
    /// <summary>
    /// Delete an existing ticket.
    /// </summary>
    /// <param name="id">The ID of the ticket that is being deleted.</param>
    /// <response code="204">Ticket deleted.</response>
    /// <response code="404">Ticket not found.</response>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            await ticketService.Delete(id);
            return NoContent(); //Returns 204 (No Content) status code if ticket is deleted successfully. 
        }
        catch (TicketNotFoundException)
        {
            //Returns 404 (Not Found) status code if ticket is not found.
            return NotFound($"Could not find ticket with id: {id}.");
        }
    }
}