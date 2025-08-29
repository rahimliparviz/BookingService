using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.Spesifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookingService.Infrastructure
{
    public class DataCleanUpBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DataCleanUpBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var homeRepository = scope.ServiceProvider.GetRequiredService<IHomeRepository>();

                    var now = DateTime.UtcNow.Date;

                    var staleHomeSpesification = new HomeWithOlderThanDateSlotSpecification(now);
                    var homes = await homeRepository.GetAllAsync(staleHomeSpesification);


                    foreach (var home in homes)
                    {
                        home.AvailableSlots.RemoveAll(slot => slot < now);

                        if (!home.AvailableSlots.Any())
                        {
                            homeRepository.Remove(home.Id);
                        }
                    }

                    var a = await homeRepository.GetAllAsync();

                }

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }
}
