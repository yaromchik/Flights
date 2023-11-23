using Flights.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.Domain.Entities;
using Project3.Domain.Errors;
using Project3.Dtos;
using Project3.ReadModels;
using System.Security.Cryptography.X509Certificates;

namespace Project3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {

        private readonly Entities _entities;

        private readonly ILogger<FlightController> _logger;

        public FlightController(ILogger<FlightController> logger, Entities entities)
        {
            _logger = logger;
            _entities = entities;
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(FlightRm),200)]
        public ActionResult<FlightRm> Find(Guid id)
        {
            var flight = _entities.Flights.SingleOrDefault(c => c.Id == id);

            if(flight == null) 
                return NotFound();

            var readModel = new FlightRm(
                flight.Id,
                flight.Airline,
                flight.Price,
                new TimePlaceRm(flight.Departure.Place.ToString(), flight.Departure.Time),
                new TimePlaceRm(flight.Arrival.Place.ToString(), flight.Arrival.Time),
                flight.RemainingNumberOfSeats
                );

            return Ok(readModel);
        }


        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(IEnumerable<FlightRm>), 200)]
        public IEnumerable<FlightRm> Search([FromQuery]FlightSearchParameters @params)
        {
            _logger.LogInformation("Searching for a flight for: {Destination}", @params.Destination);

            IQueryable<Flight> flights = _entities.Flights;

            if (!string.IsNullOrWhiteSpace(@params.Destination))
                flights = flights.Where(f => f.Arrival.Place.Contains(@params.Destination));

            if (!string.IsNullOrWhiteSpace(@params.From))
                flights = flights.Where(f => f.Departure.Place.Contains(@params.From));

            if (@params.FromDate != null)
                flights = flights.Where(f => f.Departure.Time >= @params.FromDate.Value.Date);

            if (@params.ToDate != null)
                flights = flights.Where(f => f.Departure.Time >= @params.ToDate.Value.Date.AddDays(1).AddTicks(-1));

            if (@params.NumberOfPassenger != 0 && @params.NumberOfPassenger != null)
                flights = flights.Where(f => f.RemainingNumberOfSeats >= @params.NumberOfPassenger);
            else
                flights = flights.Where(f => f.RemainingNumberOfSeats >= 1);


            var flightRmList = flights
                .Select(flight => new FlightRm(
                flight.Id,
                flight.Airline,
                flight.Price,
                new TimePlaceRm(flight.Departure.Place.ToString(), flight.Departure.Time),
                new TimePlaceRm(flight.Arrival.Place.ToString(), flight.Arrival.Time),
                flight.RemainingNumberOfSeats
                ));
            return flightRmList;
        }

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(200)]
        public IActionResult Book(BookDto dto) 
        {
            //System.Diagnostics.Debug.WriteLine($"Бронирование рейса {dto.FlightId}");
            
            var flight = _entities.Flights.Where(x => x.Id == dto.FlightId).FirstOrDefault();

            if (flight == null)
                return NotFound();

           var error = flight.MakeBooking(dto.PassengerEmail, dto.NumberOfSeats);

            if (error is OverbookError)
                return Conflict(new { message = "На рейсе не хватает мест" });

            try
            {
                _entities.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return Conflict(new { message = "Ошибко бронирования. Попробуйте еще раз" });
            }

            return CreatedAtAction(nameof(Find), new { id = dto.FlightId });
        }

        
    }
}