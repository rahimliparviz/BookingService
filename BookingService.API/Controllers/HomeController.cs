using BookingService.Application.DTOs.Home;
using BookingService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class HomeController : ControllerBase
    {
       private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet("available-homes")]
        public async Task<IActionResult> Get([FromQuery]AvailableHomesRequestDto availableHomesRequest)
        {
            var availableHomes = await _homeService.GetAvailableHomesAsync(availableHomesRequest);

            if (!availableHomes.Any()) return NotFound();

            return Ok(availableHomes);
        }
    }
}
