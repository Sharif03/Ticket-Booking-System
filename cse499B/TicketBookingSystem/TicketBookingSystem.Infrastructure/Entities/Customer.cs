using System;
using TicketBookingSystem.Infrastructure.Entities;

namespace TicketBookingSystem.Entities
{
    public class Customer : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
}
