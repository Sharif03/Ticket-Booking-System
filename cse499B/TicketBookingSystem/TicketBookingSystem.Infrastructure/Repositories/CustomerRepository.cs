using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.Infrastructure.DbContexts;
using TicketBookingSystem.Infrastructure.Entities;

namespace TicketBookingSystem.Infrastructure.Repositories
{
    public class CustomerRepository : Repository<Customer, int>,
        ICustomerRepository
    {
        public CustomerRepository(IApplicationDbContext context) 
            : base((DbContext)context)
        {
           
        }
    }
}
