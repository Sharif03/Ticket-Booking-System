using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.Services;
using Autofac;
using TicketBookingSystem.Booking.BusinessObjects;
using TicketBookingSystem.Web.Models;

namespace TicketBookingSystem.Web.Areas.Admin.Models
{
    public class TicketListModel
    {
        private readonly ITicketService _ticketService;
        public IList<Ticket> Tickets { get; set; } 

        public TicketListModel() 
        {
            _ticketService = Startup.AutofacContainer.Resolve<ITicketService>();
        }

        
    }
}
