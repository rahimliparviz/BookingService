using BookingService.Domain;

namespace BookingService.Application.Interfaces.Repositories
{
    public interface IHomeRepository
    {
        Task<List<Home>> GetAllAsync(ISpecification<Home> specification = null);
        void Remove(int id);

    }
}
