using BookingService.Application.Interfaces;
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
                    Id = 1,
                    Name = "Home One",
                    AvailableSlots = new List<DateTime>
                    {
                        new DateTime(2025, 7, 1),
                        new DateTime(2025, 7, 2),

                    }
                });

                _homes.Add(new Home
                    {
                        Id = 2,
                        Name = "Home Two",
                        AvailableSlots = new List<DateTime>
                        {
                            new DateTime(2025, 7, 3),
                            new DateTime(2025, 7, 7),
                        }
                });

                _homes.Add(new Home
                {
                    Id = 3,
                    Name = "Home Three",
                    AvailableSlots = new List<DateTime>
                    {
                        new DateTime(2025, 7, 1),
                        new DateTime(2025, 7, 4),
                        new DateTime(2025, 7, 5),
                        new DateTime(2025, 7, 6),
                    }
                });

            _homes.Add(new Home
            {
                Id = 4,
                Name = "Home Four",
                AvailableSlots = new List<DateTime>
                    {
                        new DateTime(2025, 9, 9),
                        new DateTime(2025, 10, 10),
                    }
            });

            _homes.Add(new Home
            {
                Id = 5,
                Name = "Home Five",
                AvailableSlots = new List<DateTime>
                    {
                        new DateTime(2025, 7, 15),
                        new DateTime(2025, 9, 18),
                    }
            });
        }

        public async Task<List<Home>> GetAllAsync(ISpecification<Home> specification)
        {
            if (specification is null)
            {
                return await Task.Run(() => _homes.ToList());
            }

            return await Task.Run(() =>
            {
                return _homes.AsQueryable().Where(specification.Criteria).ToList();
            });
        }

        public void Remove(int id)
        {
            var home = _homes.FirstOrDefault(h => h.Id == id);
            if (home != null)
            {
                _homes.Remove(home);
            }
        }
    }
}
