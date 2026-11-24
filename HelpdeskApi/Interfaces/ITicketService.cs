using HelpdeskApi.Dtos;

namespace HelpdeskApi.Interfaces
{
    public interface ITicketService
    {
        Task<List<TicketReadDto>> GetAllAsync();
        Task<TicketReadDto?> GetByIdAsync(int id);
        Task<(bool Success, string? Error, TicketReadDto? Ticket)> CreateAsync(CreateTicketDto dto);
        Task<(bool Success, bool Conflict, string? Error)> UpdateAsync(int id, UpdateTicketDto dto);
    }
}