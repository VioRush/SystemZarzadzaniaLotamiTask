using System.ComponentModel.DataAnnotations;

namespace SystemZarzadzaniaLotami.DTO
{
    public class UpdateFlightDTO
    {
        [Required(ErrorMessage = "Flight number is required")]
        [Range(0, 10000000, ErrorMessage = "Enter number less than 10000000")]
        public int FlightNumber { get; set; }

        [Required(ErrorMessage = "Flight date is required")]
        [DataType(DataType.DateTime, ErrorMessage = "Should be DateTime")]
        public DateTime DepartureDate { get; set; }

        [Required(ErrorMessage = "Departure airport is required")]
        [MinLength(3, ErrorMessage = "Name of departure airport must be al least 3 characters")]
        [MaxLength(100, ErrorMessage = "Name of departure airport must be max 100 characters")]
        public string DepartureAirport { get; set; }

        [Required(ErrorMessage = "Destination airport is required")]
        [MinLength(3, ErrorMessage = "Name of destination airport must be al least 3 characters")]
        [MaxLength(100, ErrorMessage = "Name of destination airport must be max 100 characters")]
        public string DestinationAirport { get; set; }

        [Required(ErrorMessage = "Plane type is required")]
        [MinLength(3, ErrorMessage = "Plane type must be al least 3 characters")]
        [MaxLength(40, ErrorMessage = "Plane type must be max 40 characters")]
        public string PlaneType { get; set; }
    }
}
