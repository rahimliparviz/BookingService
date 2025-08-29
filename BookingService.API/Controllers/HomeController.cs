using BookingService.Application.DTOs.Home;
using BookingService.Application.Interfaces.Services;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using UserService.Controllers;

namespace BookingService.API.Controllers
{
  
    public class HomeController : BaseController
    {
       private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet("available-homes")]
        public async Task<IActionResult> Get([FromQuery]AvailableHomesRequestDto availableHomesRequest)
        {
            var result = await _homeService.GetAvailableHomesAsync(availableHomesRequest);

            return result.Match(
                Success => Ok(result.Value),
                Errors => Problem(Errors)
            );
        }
    }
}
