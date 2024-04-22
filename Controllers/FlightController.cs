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
            var flight = await flightRepository.GetFlightAsync(id);

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
            await flightRepository.AddFlightAsync(flightModel);

            return CreatedAtAction(nameof(GetFlight), new { id = flightModel.Id }, flightModel.ToFlightsDTO());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateFlight([FromRoute] int id, [FromBody] UpdateFlightDTO updateDTO)
        {
            var flightModel = await flightRepository.UpdateFlightAsync(id,updateDTO);
            if (flightModel == null)
            {
                return NotFound();
            }

            return Ok(flightModel.ToFlightsDTO());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteFlight([FromRoute] int id)
        {
            var flightModel = await flightRepository.DeleteFlightAsync(id);
            if (flightModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
