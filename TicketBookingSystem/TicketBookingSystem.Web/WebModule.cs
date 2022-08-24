using Autofac;
using Autofac.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using TicketBookingSystem.Web.Areas.Admin.Models;

namespace TicketBookingSystem.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TicketListModel>().AsSelf();
                
            base.Load(builder);
        }
    }
}