using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingService.Application.DTOs.Home;

namespace BookingService.Application.Interfaces.Services
{
    public interface IHomeService
    {
        Task<List<AvailableHomesResponseDto>> GetAvailableHomesAsync(AvailableHomesRequestDto requestDto);
    }
}
