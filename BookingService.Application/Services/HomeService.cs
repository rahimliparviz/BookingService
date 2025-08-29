using BookingService.Application.DTOs.Home;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.Interfaces.Services;
using BookingService.Application.Spesifications;
using ErrorOr;

namespace BookingService.Application.Services
{
    public class HomeService : IHomeService
    {
        private readonly IHomeRepository _homeRepository;
        public HomeService(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }
        public async Task<ErrorOr<List<AvailableHomesResponseDto>>> GetAvailableHomesAsync(AvailableHomesRequestDto requestDto)
        {
            if (requestDto.StartDate > requestDto.EndDate)
            {
                return Error.Validation(description:"Start date cant be greater than end date");
            }

            var specification = new AvailableHomeInRangeSpecification(requestDto.StartDate, requestDto.EndDate);

            var data = await _homeRepository.GetAllAsync(specification);

            if (!data.Any())
            {
                return Error.NotFound(description: "Available homes do not found");
            }

            var result = data.Select(h => new AvailableHomesResponseDto(h.Id, h.Name, h.AvailableSlots)).ToList();

            return result;

        }
    }
}
