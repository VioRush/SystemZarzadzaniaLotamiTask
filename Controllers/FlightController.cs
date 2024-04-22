using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IFlightRepository flightRepository;

        public FlightController(ILogger<FlightController> logger, DatabaseContext context, IFlightRepository flightRepo)
        {
            flightRepository = flightRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlights()
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }
            
            var flights = await flightRepository.GetFlightsAsync();
            var flightsDTO = flights.Select(s=>s.ToFlightsDTO());
            return Ok(flights);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Flight>> GetFlight(int id)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var flight = await flightRepository.GetFlightAsync(id);

            if (flight == null) 
            {
                return NotFound();
            }

            return Ok(flight.ToFlightsDTO());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Flight>> AddFlight([FromBody] AddFlightDTO flightDTO)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var flightModel = flightDTO.ToFlightFromAddDTO();

            await flightRepository.AddFlightAsync(flightModel);

            return CreatedAtAction(nameof(GetFlight), new { id = flightModel.Id }, flightModel.ToFlightsDTO());
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateFlight([FromRoute] int id, [FromBody] UpdateFlightDTO updateDTO)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var flightModel = await flightRepository.UpdateFlightAsync(id,updateDTO);
            if (flightModel == null)
            {
                return NotFound();
            }

            return Ok(flightModel.ToFlightsDTO());
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteFlight([FromRoute] int id)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var flightModel = await flightRepository.DeleteFlightAsync(id);
            if (flightModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
