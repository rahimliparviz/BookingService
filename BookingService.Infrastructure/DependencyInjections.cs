using BookingService.Application.Interfaces.Repositories;
using BookingService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Infrastructure
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IHomeRepository, InMemoryHomeRepository>();

            return services;
        }
    }
}
