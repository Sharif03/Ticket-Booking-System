using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketBookingSystem.Web.Models
{
    public class SmtpConfiguration
    {
        public String Host { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Port { get; set; }
    }
}
