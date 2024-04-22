using SystemZarzadzaniaLotami.DTO;
using SystemZarzadzaniaLotami.Models;

namespace SystemZarzadzaniaLotami.Repositories
{
    public interface IFlightRepository
    {
        Task<List<Flight>> GetFlightsAsync();
        Task<Flight?> GetFlightAsync(int id);
        Task<Flight> AddFlightAsync(Flight flightModel);
        Task<Flight?> UpdateFlightAsync(int id, UpdateFlightDTO updateFlightDTO);
        Task<Flight> DeleteFlightAsync(int id);
    }
}
