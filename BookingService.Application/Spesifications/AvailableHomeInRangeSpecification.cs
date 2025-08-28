using System.Linq.Expressions;
using BookingService.Application.Interfaces;
using BookingService.Domain;

namespace BookingService.Application.Spesifications
{
    public class AvailableHomeInRangeSpecification : ISpecification<Home>
    {

        public Expression<Func<Home, bool>> Criteria { get; }

        public AvailableHomeInRangeSpecification(DateTime startDate, DateTime endDate)
        {
            HashSet<DateTime> requiredDates = Enumerable.Range(0, (endDate - startDate).Days + 1)
                .Select(offset => startDate.AddDays(offset))
                .ToHashSet();

            Criteria = home => requiredDates.All(d => home.AvailableSlots.Contains(d));
        }

      
    }
}
