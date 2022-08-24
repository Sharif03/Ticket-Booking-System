using System;

namespace TicketBookingSystem.Booking.Exceptions
{
    public class DuplicateDestinationException : Exception
    {
        public DuplicateDestinationException(string message) : base(message)
        {


        }
    }
}
