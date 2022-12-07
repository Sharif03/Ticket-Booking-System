using System;

namespace TicketBookingSystem.Infrastructure.Exceptions
{
    public class DuplicateDestinationException : Exception
    {
        public DuplicateDestinationException(string message) : base(message)
        {
        }
    }
}
