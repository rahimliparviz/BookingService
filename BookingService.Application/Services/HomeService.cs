using BookingService.Application.DTOs.Home;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.Interfaces.Services;
using BookingService.Application.Spesifications;

namespace BookingService.Application.Services
{
    public class HomeService : IHomeService
    {
        private readonly IHomeRepository _homeRepository;
        public HomeService(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }
        public async Task<List<AvailableHomesResponseDto>> GetAvailableHomesAsync(AvailableHomesRequestDto requestDto)
        {
            var specification = new AvailableHomeInRangeSpecification(requestDto.StartDate, requestDto.EndDate);

            var data = await _homeRepository.GetAllAsync(specification);

            var result = data.Select(h => new AvailableHomesResponseDto(h.Id, h.Name, h.AvailableSlots)).ToList();

            return result;

        }
    }
}
