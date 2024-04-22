using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaLotami.DTO;
using SystemZarzadzaniaLotami.Mappers;
using SystemZarzadzaniaLotami.Models;
using SystemZarzadzaniaLotami.Repositories;

namespace SystemZarzadzaniaLotami.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly DatabaseContext db;
        private readonly IFlightRepository flightRepository;

        public FlightController(DatabaseContext context, IFlightRepository flightRepo)
        {
            flightRepository = flightRepo;
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlights()
        {
            var flights = await flightRepository.GetFlightsAsync();
            var flightsDTO = flights.Select(s=>s.ToFlightsDTO());
            return Ok(flights);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlight(int id)
        {
            if (db.Flights == null) { return NotFound(); }
            var flight = await db.Flights.FindAsync(id);

            if (flight == null) 
            {
                return NotFound();
            }

            return Ok(flight.ToFlightsDTO());
        }

        [HttpPost]
        public async Task<ActionResult<Flight>> AddFlight([FromBody] AddFlightDTO flightDTO)
        {
            var flightModel = flightDTO.ToFlightFromAddDTO();
            await db.Flights.AddAsync(flightModel);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFlight), new { id = flightModel.Id }, flightModel.ToFlightsDTO());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateFlight([FromRoute] int id, [FromBody] UpdateFlightDTO updateDTO)
        {
            var flightModel = await db.Flights.FirstOrDefaultAsync(e => e.Id == id);
            if (flightModel == null)
            {
                return NotFound();
            }

            flightModel.FlightNumber = updateDTO.FlightNumber;
            flightModel.DepartureDate = updateDTO.DepartureDate;
            flightModel.DepartureAirport = updateDTO.DepartureAirport;
            flightModel.DestinationAirport = updateDTO.DestinationAirport;
            flightModel.PlaneType = updateDTO.PlaneType;
            await db.SaveChangesAsync();

            return Ok(flightModel.ToFlightsDTO());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteFlight([FromRoute] int id)
        {
            var flightModel = await db.Flights.FirstOrDefaultAsync(e => e.Id == id);
            if (flightModel == null)
            {
                return NotFound();
            }

            db.Flights.Remove(flightModel);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool FlightExists(int id)
        {
            return (db.Flights?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
