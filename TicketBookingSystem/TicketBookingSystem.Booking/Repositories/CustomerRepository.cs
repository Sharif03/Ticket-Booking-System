using TicketBookingSystem.Data;
using TicketBookingSystem.Booking.Entities;
using Microsoft.EntityFrameworkCore;

namespace TicketBookingSystem.Booking.Repositories
{
    public class CustomerRepository : Repository<Customer, int>,
        ICustomerRepository
    {
        public CustomerRepository(IBookingDbContext context) 
            : base((DbContext)context)
        {
           
        }
    }
}
