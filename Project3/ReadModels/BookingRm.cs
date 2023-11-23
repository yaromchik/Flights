using Project3.Domain.Entities;

namespace Project3.ReadModels
{
    public record BookingRm(Guid FlightId,
        string Airline,
        string Price,
        TimePlaceRm Departure,
        TimePlaceRm Arrival,
        int NumberOfBookedSeats,
        string PassengerEmail);
}
