using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingSystem.Booking;
using TicketBookingSystem.Common;
using TicketBookingSystem.Web.Data;
using TicketBookingSystem.Web.Models;

namespace TicketBookingSystem.Web
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            WebHostEnvironment = environment;
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; set; }
        public static ILifetimeScope AutofacContainer { get; set; }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var connectionInfo = GenConnectionStringAndAssemblyName();
            builder.RegisterModule(new BookingModule(connectionInfo.connectionString, connectionInfo.migrationAssemblyName));

            builder.RegisterModule(new CommonModule());

            builder.RegisterModule(new WebModule());
        }

        private (string connectionString, string migrationAssemblyName) GenConnectionStringAndAssemblyName ()
        {
            var connectionStringName = "DefaultConnection";
            var connectionString = Configuration.GetConnectionString(connectionStringName);
            var migrationAssemblyName = typeof(Startup).Assembly.FullName;
            
            return (connectionString, migrationAssemblyName);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionInfo = GenConnectionStringAndAssemblyName();
            
            services.AddDbContext<BookingDbContext>(options =>
                options.UseSqlServer(connectionInfo.connectionString,
                b => b.MigrationsAssembly(connectionInfo.migrationAssemblyName)));

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(connectionInfo.connectionString,
                b => b.MigrationsAssembly(connectionInfo.migrationAssemblyName)));

            services.AddDefaultIdentity<IdentityUser>(options => 
                options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

           
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

                options.LoginPath = "/Account/Signin";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddSession(options =>
            {
                // Session settings
                options.IdleTimeout = TimeSpan.FromSeconds(100);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            
            services.Configure<SmtpConfiguration>(Configuration.GetSection("Smtp"));

            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

        }

 
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Ticket}/{action=Index}/{Id?}");
                //  pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{Id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
