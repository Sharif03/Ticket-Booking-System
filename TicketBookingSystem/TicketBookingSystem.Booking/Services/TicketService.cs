using System.Collections.Generic;
using System.Linq;
using TicketBookingSystem.Booking.UnitOfWorks;
using TicketBookingSystem.Booking.BusinessObjects;
using System;
using TicketBookingSystem.Booking.Exceptions;

namespace TicketBookingSystem.Booking.Services
{
    public class TicketService : ITicketService
    {
        private readonly IBookingUnitOfWork _bookingUnitOfWork;
        public TicketService(IBookingUnitOfWork bookingUnitOfWork)
        {
           _bookingUnitOfWork = bookingUnitOfWork;
        }
        public IList<Ticket> GetAllTickets() 
        {
            var ticketEntities = _bookingUnitOfWork.Tickets.GetAll();

            var tickets = new List<Ticket>();
            foreach (var entity in ticketEntities)
            {
                var ticket = new Ticket()
                {
                    CustomerId = entity.CustomerId,
                    Destination = entity.Destination,
                    TicketFee = entity.TicketFee
                };
                tickets.Add(ticket);
            }
            return tickets;
        }

        public void CreateTicket(Ticket ticket)
        {
            if (ticket == null)
                throw new InvalidParameterException("Ticket was not provided");

            if (!IsDestinationAlreadyBooked(ticket.Destination))
            {

                _bookingUnitOfWork.Tickets.Add(
                    new Entities.Ticket
                    {
                        CustomerId = ticket.CustomerId,
                        Destination = ticket.Destination,
                        TicketFee = ticket.TicketFee
                    }
                 );

                _bookingUnitOfWork.Save();
            }
            else
                throw new DuplicateDestinationException("Destination already Booked");

        }

        public void BuyTicket(Ticket ticket, Customer customer)
        {
            var ticketEntity = _bookingUnitOfWork.Tickets.GetById(ticket.Id);
            
            if (ticketEntity == null)
                throw new InvalidOperationException("Ticket is not Found");

        }

        private bool IsDestinationAlreadyBooked(string destination) => 
             _bookingUnitOfWork.Tickets.GetCount(x => x.Destination == destination) > 0;

        private bool IsDestinationAlreadyBooked(string destination, int id) =>
             _bookingUnitOfWork.Tickets.GetCount(x => x.Destination == destination && x.Id != id) > 0;


        public (IList<Ticket> records, int total, int totalDisplay) GetTickets(int pageIndex, int pageSize, string searchText, string sortText)
        {
 
            var ticketData  = _bookingUnitOfWork.Tickets.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : (x => x.Destination.Contains(searchText)), 
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from ticket in ticketData.data
                          select new Ticket
                          {
                              CustomerId = ticket.CustomerId,
                              Destination = ticket.Destination,
                              TicketFee = ticket.TicketFee,
                              Id = ticket.Id,
                          }).ToList();
            
            return (resultData, ticketData.total, ticketData.totalDisplay);
        }

        public Ticket GetTicket(int id)
        {
            var ticket = _bookingUnitOfWork.Tickets.GetById(id);

            if (ticket == null)
                return null;

            return new Ticket
            {
                Id = ticket.Id,
                CustomerId = ticket.CustomerId,
                Destination = ticket.Destination,
                TicketFee = ticket.TicketFee
            };
        }

        public void UpdateTicket(Ticket ticket)
        {
            if (ticket == null)
                throw new InvalidOperationException("Ticket is missing");

            if(IsDestinationAlreadyBooked(ticket.Destination, ticket.Id))
                throw new DuplicateDestinationException("Already Booked");

            var ticketEntity =  _bookingUnitOfWork.Tickets.GetById(ticket.Id);
            if (ticketEntity != null)
            {
                ticketEntity.CustomerId = ticket.CustomerId;
                ticketEntity.Destination = ticket.Destination;
                ticketEntity.TicketFee = ticket.TicketFee;

                _bookingUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find ticket");
        }

        public void DeleteTicket(int id)
        {
            _bookingUnitOfWork.Tickets.Remove(id);
            _bookingUnitOfWork.Save();
        }
    }
}
