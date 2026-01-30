using System.ComponentModel.DataAnnotations;

namespace SUPREA_LOGISTICS.ViewModels
{
    public class BookingViewModel
    {
        [Required]
        public string Region { get; set; } = string.Empty;
        public string? RegionOther { get; set; }
        [Required]
        public string NatureOfRequest { get; set; } = string.Empty;

        public IFormFile? Document { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateNeeded { get; set; }

        [Required]
        public string RequestedBy { get; set; } = string.Empty;
    }
}
