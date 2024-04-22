using Microsoft.EntityFrameworkCore;
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

        public Task<List<Flight>> GetFlightsAsync()
        {
            return db.Flights.ToListAsync();
        }
    }
}
