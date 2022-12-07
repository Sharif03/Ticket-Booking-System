using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Infrastructure.DbContexts;
using TicketBookingSystem.Infrastructure.Repositories;

namespace TicketBookingSystem.Infrastructure.UnitOfWorks
{
    public class BookingUnitOfWork : UnitOfWork, IBookingUnitOfWork
    {
        public ICustomerRepository Customers { get; private set; }
        public ITicketRepository Tickets { get; private set; }
        public BookingUnitOfWork(IApplicationDbContext context,
            ICustomerRepository customers,
            ITicketRepository tickets
            ) : base((DbContext)context)
        {
            Customers = customers;
            Tickets = tickets;
        }
    }
}
