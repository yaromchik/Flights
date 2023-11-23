using System.ComponentModel;

namespace Flights.Dtos
{
    public record FlightSearchParameters(
        [DefaultValue("11/22/2023 10:30:00 AM")]
        DateTime? FromDate,
        [DefaultValue("11/23/202310:30:00 AM")]
        DateTime? ToDate,
        [DefaultValue("Tomsk")]
        string? Destination,
        [DefaultValue("Omsk")]
        string? From,
        [DefaultValue(1)]
        int? NumberOfPassenger
    );
}
