using GloboTicket.TicketManagement.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GloboTicket.TicketManagement.Api.IntegrationTests.Base
{
    public class CustomWebApplicationFactory<TProgram>
        : WebApplicationFactory<TProgram> where TProgram : class
    {
        private static readonly InMemoryDatabaseRoot InMemoryDatabaseRoot = new();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<GloboTicketDbContext>));
                services.RemoveAll(typeof(IDbContextOptionsConfiguration<GloboTicketDbContext>));

                var sqlServerDescriptors = services
                    .Where(d =>
                        (d.ServiceType?.Namespace?.Contains("SqlServer") ?? false) ||
                        (d.ImplementationType?.Namespace?.Contains("SqlServer") ?? false))
                    .ToList();

                foreach (var descriptor in sqlServerDescriptors)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<GloboTicketDbContext>(options =>
                {
                    options.UseInMemoryDatabase("GloboTicketDbContextInMemoryTest");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<GloboTicketDbContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TProgram>>>();

                    context.Database.EnsureCreated();

                    try
                    {
                        Utilities.InitializeDbForTests(context);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(
                            $"An error occurred seeding the database with test messages. Error: {ex.Message}");
                    }
                }
            });

            base.ConfigureWebHost(builder);
        }

        public HttpClient GetAnonymousClient()
        {
            return CreateClient();
        }
    }
}
