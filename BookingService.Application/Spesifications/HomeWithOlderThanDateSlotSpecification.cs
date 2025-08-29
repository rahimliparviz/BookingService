using System.Linq.Expressions;
using BookingService.Application.Interfaces;
using BookingService.Domain;

namespace BookingService.Application.Spesifications
{
    public class HomeWithOlderThanDateSlotSpecification : ISpecification<Home>
    {

        public Expression<Func<Home, bool>> Criteria { get; }

        public HomeWithOlderThanDateSlotSpecification(DateTime dateTime)
        {
            Criteria = home => home.AvailableSlots.Any(slot => slot < dateTime);
        }

      
    }
}
