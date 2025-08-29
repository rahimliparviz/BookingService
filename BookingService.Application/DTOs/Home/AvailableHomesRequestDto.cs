using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace BookingService.Application.DTOs.Home
{
    public record AvailableHomesRequestDto
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; init; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; init; }

    }
}
