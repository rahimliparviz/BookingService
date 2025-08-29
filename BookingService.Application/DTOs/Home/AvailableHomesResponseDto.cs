using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.DTOs.Home
{
    public record AvailableHomesResponseDto(int HomeId, string HomeName, List<DateTime> AvailableSlots);
}
