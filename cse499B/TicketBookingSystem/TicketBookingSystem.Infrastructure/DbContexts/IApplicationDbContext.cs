using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.Infrastructure.Entities;

namespace TicketBookingSystem.Infrastructure.DbContexts
{
    public interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Ticket> Tickets { get; set; }
    }
}