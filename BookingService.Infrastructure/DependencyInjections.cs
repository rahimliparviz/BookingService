using BookingService.Application.Interfaces.Repositories;
using BookingService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookingService.Infrastructure
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.Configure<HostOptions>(x =>
            {
                x.ServicesStartConcurrently = true;
                x.ServicesStopConcurrently = true;
            });


            services.AddScoped<IHomeRepository, InMemoryHomeRepository>();
            services.AddHostedService<DataCleanUpBackgroundService>();


            return services;
        }
    }
}
