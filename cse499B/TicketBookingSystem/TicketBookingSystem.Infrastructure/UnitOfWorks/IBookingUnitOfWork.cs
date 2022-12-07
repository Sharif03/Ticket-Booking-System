using TicketBookingSystem.Infrastructure.Repositories;

namespace TicketBookingSystem.Infrastructure.UnitOfWorks
{
    public interface IBookingUnitOfWork : IUnitOfWork
    {
         public ICustomerRepository Customers { get; }
         public ITicketRepository Tickets { get; }
    }
}
