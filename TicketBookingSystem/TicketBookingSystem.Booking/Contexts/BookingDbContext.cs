using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.Booking.Entities;

namespace TicketBookingSystem.Booking
{
    public class BookingDbContext : DbContext, IBookingDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
        public BookingDbContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(_connectionString, m => m.MigrationsAssembly(_migrationAssemblyName));
            }
            base.OnConfiguring(dbContextOptionsBuilder);
        }

        /*
        // Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {

            modelBuilder.Entity<Ticket>()
                .HasMany<Customer>()
                .WithOne(c => c.Customer);


            base.OnModelCreating(modelBuilder);
        }
        */

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    
    }
}
