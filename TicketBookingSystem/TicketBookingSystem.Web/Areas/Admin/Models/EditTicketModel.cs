using TicketBookingSystem.Booking.Services;
using Autofac;
using TicketBookingSystem.Booking.BusinessObjects;
using System;
using System.ComponentModel.DataAnnotations;

namespace TicketBookingSystem.Web.Areas.Admin.Models
{
    public class EditTicketModel
    {
        [Required]
        public int? Id { get; set; }
        [Required]
        public int? CustomerId { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Destination length should less then 200 charecters")]
        public string? Destination { get; set; }
        [Required, Range(100, 1000)]
        public float? TicketFee { get; set; }

        private readonly ITicketService _ticketService;
        public EditTicketModel()
        {
            _ticketService = Startup.AutofacContainer.Resolve<ITicketService>();
        }

       
    }
}
