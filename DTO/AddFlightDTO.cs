﻿namespace SystemZarzadzaniaLotami.DTO
{
    public class AddFlightDTO
    {
        public int FlightNumber { get; set; }

        public DateTime DepartureDate { get; set; }

        public string DepartureAirport { get; set; }

        public string DestinationAirport { get; set; }

        public string PlaneType { get; set; }
    }
}