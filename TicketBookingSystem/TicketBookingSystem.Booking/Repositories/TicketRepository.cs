using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Data;
using TicketBookingSystem.Booking.Entities;
using Microsoft.EntityFrameworkCore;

namespace TicketBookingSystem.Booking.Repositories
{
    public class TicketRepository : Repository<Ticket, int>, 
        ITicketRepository
    {
        public TicketRepository(IBookingDbContext context) 
            : base((DbContext)context)
        {
           
        }
    }
}
