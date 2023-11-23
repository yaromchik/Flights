using System.ComponentModel.DataAnnotations;

namespace Project3.Dtos
{
    public record BookDto(
        [Required] Guid FlightId,
        [Required] [EmailAddress] string PassengerEmail,
        [Required] [Range(1,254)] byte NumberOfSeats);
}
