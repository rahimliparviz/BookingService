using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingService.Domain;

namespace BookingService.Infrastructure.Data
{
    public class InMemoryHomeDataStore 
    {
        private readonly List<Home> _homes = new List<Home>();

        public InMemoryHomeDataStore()
        {
            _homes.Add(new Home
            {
                Id = 1,
                Name = "Home One",
                AvailableSlots = new List<DateTime>
                    {
                        new DateTime(2025, 9, 10),
                        new DateTime(2025, 9, 12),

                    }
            });

            _homes.Add(new Home
            {
                Id = 2,
                Name = "Home Two",
                AvailableSlots = new List<DateTime>
                        {
                            new DateTime(2025, 9, 13),
                            new DateTime(2025, 9, 17),
                        }
            });

            _homes.Add(new Home
            {
                Id = 3,
                Name = "Home Three",
                AvailableSlots = new List<DateTime>
                    {
                        new DateTime(2025, 10, 1),
                        new DateTime(2025, 10, 4),
                        new DateTime(2025, 10, 5),
                        new DateTime(2025, 10, 6),
                    }
            });

            _homes.Add(new Home
            {
                Id = 4,
                Name = "Home Four",
                AvailableSlots = new List<DateTime>
                    {
                        new DateTime(2025, 10, 9),
                        new DateTime(2025, 10, 10),
                    }
            });

            _homes.Add(new Home
            {
                Id = 5,
                Name = "Home Five",
                AvailableSlots = new List<DateTime>
                    {
                        new DateTime(2025, 10, 9),
                        new DateTime(2025, 10, 15),
                        new DateTime(2025, 10, 18),
                    }
            });

            _homes.Add(new Home
            {
                Id = 1,
                Name = "Home Six",
                AvailableSlots = new List<DateTime>
                    {
                        new DateTime(2025, 7, 1),
                        new DateTime(2025, 7, 2),

                    }
            });

            _homes.Add(new Home
            {
                Id = 2,
                Name = "Home Seven",
                AvailableSlots = new List<DateTime>
                        {
                            new DateTime(2025, 7, 3),
                            new DateTime(2025, 7, 7),
                        }
            });

            _homes.Add(new Home
            {
                Id = 3,
                Name = "Home Eight",
                AvailableSlots = new List<DateTime>
                    {
                        new DateTime(2025, 7, 1),
                        new DateTime(2025, 7, 4),
                        new DateTime(2025, 7, 5),
                        new DateTime(2025, 7, 6),
                    }
            });
        }

        public void AddHome(Home home)
        {
            _homes.Add(home);
        }

        public void Clear()
        {
            _homes.Clear();
        }

        public List<Home> GetAllHomes()
        {
            return _homes;
        }
    }
}
