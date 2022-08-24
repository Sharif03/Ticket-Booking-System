using TicketBookingSystem.Booking.Services;
using Autofac;
using TicketBookingSystem.Booking.BusinessObjects;
using System.ComponentModel.DataAnnotations;

namespace TicketBookingSystem.Web.Areas.Admin.Models
{
    public class CreateTicketModel
    {
        [Required]
        public int CustomerId { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Destination length should less then 200 charecters")]
        public string Destination { get; set; }
        [Required, Range(100, 1000)]
        public float TicketFee { get; set; }

        private readonly ITicketService _ticketService;
        public CreateTicketModel()
        {
            _ticketService = Startup.AutofacContainer.Resolve<ITicketService>();
        }

    }
}
