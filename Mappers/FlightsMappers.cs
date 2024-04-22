using SystemZarzadzaniaLotami.DTO;
using SystemZarzadzaniaLotami.Models;

namespace SystemZarzadzaniaLotami.Mappers
{
    public static class FlightsMappers
    {
        public static FlightsDTO ToFlightsDTO(this Flight flight)
        {
            return new FlightsDTO { 
                Id = flight.Id,
                FlightNumber = flight.FlightNumber,
                DepartureDate = flight.DepartureDate,
                DepartureAirport = flight.DepartureAirport,
                DestinationAirport = flight.DestinationAirport,
                PlaneType = flight.PlaneType
            };
        }

        public static Flight ToFlightFromAddDTO(this AddFlightDTO flightDTO)
        {
            return new Flight
            {
                FlightNumber = flightDTO.FlightNumber,
                DepartureDate = flightDTO.DepartureDate,
                DepartureAirport = flightDTO.DepartureAirport,
                DestinationAirport = flightDTO.DestinationAirport,
                PlaneType = flightDTO.PlaneType
            };
        }
    }
}
