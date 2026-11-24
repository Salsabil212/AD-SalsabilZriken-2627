using Microsoft.EntityFrameworkCore;
using HelpdeskApi.Data;
using HelpdeskApi.Interfaces;
using HelpdeskApi.Models;

namespace HelpdeskApi.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly HelpdeskDbContext _context;
        public TicketRepository(HelpdeskDbContext context) => _context = context;

        public async Task<List<Ticket>> GetAllAsync() => await _context.Tickets.Include(t => t.Category).ToListAsync();
        public async Task<Ticket?> GetByIdAsync(int id) => await _context.Tickets.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == id);
        public async Task AddAsync(Ticket ticket) => await _context.Tickets.AddAsync(ticket);
        public void Update(Ticket ticket) => _context.Tickets.Update(ticket);
        public void Delete(Ticket ticket) => _context.Tickets.Remove(ticket);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}