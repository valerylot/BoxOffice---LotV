using System.ComponentModel.DataAnnotations;

namespace BoxOffice___LotV.DTOs
{
    public class ConcertResponseDTO
    {
        public int Id {get; set;}

        [Required(ErrorMessage = "Artist is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Artist must be between 1 and 100 characters.")]
        public string Artist {get; set;} = string.Empty;
       [StringLength(100, ErrorMessage = "Opening Act cannot exceed 100 characters.")]
        public string? OpeningAct {get; set;} = string.Empty;
        [Required(ErrorMessage = "Venue is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Venue must be between 1 and 100 characters.")]
        public string Venue {get; set;} = string.Empty;
        [Required(ErrorMessage = "City is required.")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "City must be between 1 and 60 characters.")]
        public string City {get; set;} = string.Empty;
        [Required(ErrorMessage = "Date is required.")]
        // [FutureDate("Date must be in the future.")]
        public DateTime Date {get; set;}
        [Required(ErrorMessage = "Genre is required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Genre must be between 1 and 50 characters.")]
        public string Genre {get; set;} = string.Empty;
        [Range(0.01, double.MaxValue, ErrorMessage = "Ticket price must be greater than 0.")]
        public double TicketPrice {get; set;}
        [Range(1, 100000, ErrorMessage = "Capacity must be between 1 and 100000.")]
        public int Capacity {get; set;}
        public int TicketsSold {get; set;}
        public int TicketsRemaining {get; set;}
    }
}