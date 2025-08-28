using System.Linq;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Domain;

namespace BookingService.Infrastructure.Repositories
{
    public class InMemoryHomeRepository: IHomeRepository
    {
        private readonly List<Home> _homes = new List<Home>();
        public InMemoryHomeRepository()
        {
                _homes.Add(new Home
                {
                    HomeId = 1,
                    HomeName = "Home One",
                    AvailableSlots = new List<DateTime>
                    {
                        new DateTime(2025, 7, 1),
                        new DateTime(2025, 7, 2),

                    }
                });

                _homes.Add(new Home
                    {
                        HomeId = 2,
                        HomeName = "Home Two",
                        AvailableSlots = new List<DateTime>
                        {
                            new DateTime(2025, 7, 3),
                            new DateTime(2025, 7, 7),
                        }
                });

                _homes.Add(new Home
                {
                    HomeId = 3,
                    HomeName = "Home Three",
                    AvailableSlots = new List<DateTime>
                    {
                        new DateTime(2025, 7, 1),
                        new DateTime(2025, 7, 4),
                        new DateTime(2025, 7, 5),
                        new DateTime(2025, 7, 6),
                    }
                });
        }

        public  Task<List<Home>> GetAvailableHomesAsync(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
