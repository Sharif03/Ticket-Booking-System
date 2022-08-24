using TicketBookingSystem.Data;

namespace TicketBookingSystem.Booking.Entities
{
    public class Ticket : IEntity<int>

    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Destination { get; set; }
        public float TicketFee { get; set; }
    }
}
