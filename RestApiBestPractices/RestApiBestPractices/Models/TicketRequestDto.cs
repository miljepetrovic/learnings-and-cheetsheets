using System.ComponentModel.DataAnnotations;

namespace RestApiBestPractices.Models;

public record TicketRequestDto(
    string Name, 
    string Description,
    string? ETag
);