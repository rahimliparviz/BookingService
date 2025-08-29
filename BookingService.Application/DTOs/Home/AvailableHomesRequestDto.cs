using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.DTOs.Home
{
    public record AvailableHomesRequestDto(DateTime StartDate, DateTime EndDate);
}
