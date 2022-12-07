using Autofac;
using TicketBookingSystem.Infrastructure.DbContexts;
using TicketBookingSystem.Infrastructure.Repositories;
using TicketBookingSystem.Infrastructure.Services;
using TicketBookingSystem.Infrastructure.UnitOfWorks;

namespace TicketBookingSystem.Infrastructure
{
    public class InfrastructureModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
        public InfrastructureModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<TicketBookingService>().As<ITicketBookingService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TicketRepository>().As<ITicketRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BookingUnitOfWork>().As<IBookingUnitOfWork>()
                 .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}