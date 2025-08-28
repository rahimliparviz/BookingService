using BookingService.Domain;

namespace BookingService.Application.Interfaces.Repositories
{
    internal interface IHomeRepository
    {
        Task<List<Home>> GetAvailableHomesAsync(DateTime startDate, DateTime endDate);

    }
}
