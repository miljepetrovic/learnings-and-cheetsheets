using RestApiBestPractices.Models;

namespace RestApiBestPractices.Services.Interfaces;

public interface ITicketService
{
    Task<IReadOnlyList<TicketResponseDto>> GetAll(int page, int pageSize);
    Task<TicketResponseDto> GetById(Guid id);
    Task<TicketResponseDto >Create(TicketRequestDto request);
    Task<TicketResponseDto> Update(Guid userId, TicketRequestDto request);
    Task Delete(Guid id);
}