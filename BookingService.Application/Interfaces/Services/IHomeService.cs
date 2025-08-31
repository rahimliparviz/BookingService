using BookingService.Application.DTOs.Home;
using ErrorOr;

namespace BookingService.Application.Interfaces.Services
{
    public interface IHomeService
    {
        Task<ErrorOr<List<AvailableHomesResponseDto>>> GetAvailableHomesAsync(AvailableHomesRequestDto requestDto);
    }
}
