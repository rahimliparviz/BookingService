using BookingService.Application.Interfaces;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Domain;
using BookingService.Infrastructure.Data;

namespace BookingService.Infrastructure.Repositories
{
    public class InMemoryHomeRepository: IHomeRepository
    {
        private readonly List<Home> _homes = new List<Home>();
        public InMemoryHomeRepository(InMemoryHomeDataStore inMemoryHomeDataStore)
        {
            _homes = inMemoryHomeDataStore.GetAllHomes();
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
