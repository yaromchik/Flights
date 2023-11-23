﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project3.Domain.Entities;
using Project3.Dtos;
using Project3.ReadModels;
using Project3.Data;
using System.Runtime.CompilerServices;

namespace Project3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {

        private readonly Entities _entities;

        public PassengerController(Entities entities)
        {
            _entities = entities;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Register(NewPassengerDto dto)
        {
            _entities.Passengers.Add(new Passenger(
                dto.Email,
                dto.FirstName,
                dto.LastName,
                dto.Gender
                ));

            _entities.SaveChanges();

            return CreatedAtAction(nameof(Find), new { email = dto.Email });
        }

        [HttpGet("{email}")]
        public ActionResult<PassengerRm> Find(string email)
        {
            var passenger = _entities.Passengers.Where(p => p.Email == email).FirstOrDefault();
            if (passenger == null) return NotFound();

            var rm = new PassengerRm(
                passenger.Email,
                passenger.FirstName,
                passenger.LastName,
                passenger.Gender
                );

            return Ok(rm);
        }
    }
}
