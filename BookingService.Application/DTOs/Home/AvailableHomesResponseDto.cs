namespace BookingService.Application.DTOs.Home
{
    public record AvailableHomesResponseDto(int HomeId, string HomeName, List<DateTime> AvailableSlots);
}
