using System.ComponentModel.DataAnnotations;

namespace Project3.Dtos
{
    public record NewPassengerDto(
       [EmailAddress] string Email,
       [Required] string FirstName,
       [Required] string LastName,
       [Required] bool Gender);
}
