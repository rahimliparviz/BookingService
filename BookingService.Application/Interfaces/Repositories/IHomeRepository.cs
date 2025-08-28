using BookingService.Domain;

namespace BookingService.Application.Interfaces.Repositories
{
    public interface IHomeRepository
    {
        Task<List<Home>> GetAvailableHomesAsync(DateTime startDate, DateTime endDate);

    }
}
