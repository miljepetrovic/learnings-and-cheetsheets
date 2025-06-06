namespace RestApiBestPractices.Models;

public record TicketResponseDto(
    Guid Id, 
    string Name, 
    string Description
);