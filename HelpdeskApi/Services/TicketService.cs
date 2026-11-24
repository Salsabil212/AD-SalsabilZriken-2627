using HelpdeskApi.Dtos;
using HelpdeskApi.Interfaces;
using HelpdeskApi.Models;

namespace HelpdeskApi.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepo;
        private readonly ICategoryRepository _categoryRepo;

        public TicketService(ITicketRepository ticketRepo, ICategoryRepository categoryRepo)
        {
            _ticketRepo = ticketRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task<List<TicketReadDto>> GetAllAsync()
        {
            var tickets = await _ticketRepo.GetAllAsync();
            return tickets.Select(ToDto).ToList();
        }

        public async Task<TicketReadDto?> GetByIdAsync(int id)
        {
            var ticket = await _ticketRepo.GetByIdAsync(id);
            return ticket == null ? null : ToDto(ticket);
        }

        public async Task<(bool Success, string? Error, TicketReadDto? Ticket)> CreateAsync(CreateTicketDto dto)
        {
            if (!await _categoryRepo.ExistsAsync(dto.CategoryId))
                return (false, $"CategoryId {dto.CategoryId} bestaat niet.", null);

            var ticket = new Ticket
            {
                CategoryId = dto.CategoryId,
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                CreatedBy = dto.CreatedBy,
                Status = "Open",
                CreatedAt = DateTime.Now
            };

            await _ticketRepo.AddAsync(ticket);
            await _ticketRepo.SaveChangesAsync();
            return (true, null, ToDto(ticket));
        }

        public async Task<(bool Success, bool Conflict, string? Error)> UpdateAsync(int id, UpdateTicketDto dto)
        {
            var ticket = await _ticketRepo.GetByIdAsync(id);
            if (ticket == null) return (false, false, "Ticket niet gevonden.");

            if (ticket.Status == "Closed")
                return (false, true, "Dit ticket is gesloten en kan niet meer bewerkt worden.");

            if (!await _categoryRepo.ExistsAsync(dto.CategoryId))
                return (false, false, $"CategoryId {dto.CategoryId} bestaat niet.");

            ticket.CategoryId = dto.CategoryId;
            ticket.Title = dto.Title;
            ticket.Description = dto.Description;
            ticket.Priority = dto.Priority;
            ticket.Status = dto.Status;

            _ticketRepo.Update(ticket);
            await _ticketRepo.SaveChangesAsync();
            return (true, false, null);
        }

        private static TicketReadDto ToDto(Ticket t) => new TicketReadDto
        {
            Id = t.Id,
            CategoryId = t.CategoryId,
            CategoryName = t.Category?.Name,
            Title = t.Title,
            Description = t.Description,
            Status = t.Status,
            Priority = t.Priority,
            CreatedBy = t.CreatedBy,
            CreatedAt = t.CreatedAt
        };
    }
}