using TicketBookingSystem.Data;
using TicketBookingSystem.Booking.Repositories;

namespace TicketBookingSystem.Booking.UnitOfWorks
{
    public interface IBookingUnitOfWork : IUnitOfWork
    {
         public ICustomerRepository Customers { get; }
         public ITicketRepository Tickets { get; }
    }
}
