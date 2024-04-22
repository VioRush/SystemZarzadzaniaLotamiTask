using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaLotami.DTO;
using SystemZarzadzaniaLotami.Models;

namespace SystemZarzadzaniaLotami.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly DatabaseContext db;
        public FlightRepository(DatabaseContext dBContext)
        {
            db = dBContext;
        }

        public async Task<List<Flight>> GetFlightsAsync()
        {
            return await db.Flights.ToListAsync();
        }

        public async Task<Flight> AddFlightAsync(Flight flightModel)
        {
            Console.WriteLine("iddddddddddd= " + flightModel.Id);
            await db.Flights.AddAsync(flightModel);
            await db.SaveChangesAsync();
            return flightModel;
        }

        public async Task<Flight> DeleteFlightAsync(int id)
        {
            var flightModel = await db.Flights.FirstOrDefaultAsync(e => e.Id == id);
           
            if (flightModel == null)
            {
                return null;
            }

            db.Flights.Remove(flightModel);
            await db.SaveChangesAsync();
            return flightModel;
        }

        public async Task<Flight?> GetFlightAsync(int id)
        {
            return await db.Flights.FindAsync(id);
        }

        public async Task<Flight?> UpdateFlightAsync(int id, UpdateFlightDTO updateDTO)
        {
            var flightModel = await db.Flights.FirstOrDefaultAsync(e => e.Id == id);
            if (flightModel == null)
            {
                return null;
            }

            flightModel.FlightNumber = updateDTO.FlightNumber;
            flightModel.DepartureDate = updateDTO.DepartureDate;
            flightModel.DepartureAirport = updateDTO.DepartureAirport;
            flightModel.DestinationAirport = updateDTO.DestinationAirport;
            flightModel.PlaneType = updateDTO.PlaneType;
            await db.SaveChangesAsync();

            return flightModel;
        }
    }
}
