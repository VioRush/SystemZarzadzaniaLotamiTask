using SystemZarzadzaniaLotami.Models;

namespace SystemZarzadzaniaLotami.Repositories
{
    public interface IFlightRepository
    {
        Task<List<Flight>> GetFlightsAsync();
    }
}
