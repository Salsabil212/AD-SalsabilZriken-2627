using Microsoft.EntityFrameworkCore;
using HelpdeskApi.Models;

namespace HelpdeskApi.Data
{
    public class HelpdeskDbContext : DbContext
    {
        public HelpdeskDbContext(DbContextOptions<HelpdeskDbContext> options) : base(options) { }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Ticket> Tickets => Set<Ticket>();
    }
}