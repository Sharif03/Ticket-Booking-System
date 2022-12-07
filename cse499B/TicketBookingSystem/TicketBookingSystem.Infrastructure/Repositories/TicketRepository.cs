using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Infrastructure.DbContexts;
using TicketBookingSystem.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace TicketBookingSystem.Infrastructure.Repositories
{
    public class TicketRepository : Repository<Ticket, int>, 
        ITicketRepository
    {
        public TicketRepository(IApplicationDbContext context) 
            : base((DbContext)context)
        {
           
        }
    }
}
