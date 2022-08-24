using Autofac;
using System;

namespace TicketBookingSystem.Common
{
    public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            /*
            builder.RegisterType<DateTimeUtility>().As<IDateTimeUtility>()
              .InstancePerLifetimeScope();
            
            base.Load(builder);
            */
        }

    }
}
