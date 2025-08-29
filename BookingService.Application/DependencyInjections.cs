using BookingService.Application.Interfaces.Services;
using BookingService.Application.Services;
using Microsoft.Extensions.DependencyInjection;


namespace BookingService.Application
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IHomeService, HomeService>();

            return services;
        }
    }
}
