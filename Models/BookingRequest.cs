using System.ComponentModel.DataAnnotations;

namespace SUPREA_LOGISTICS.Models
{
    public class BookingRequest
    {
        public int BookingRequestId { get; set; }

        public string Region { get; set; } = null!;

        public string NatureOfRequest { get; set; } = null!;

        public string? DocumentFileName { get; set; }
        public byte[]? DocumentFileData { get; set; }
        public string? DocumentContentType { get; set; }

        public DateTime DateNeeded { get; set; }

        public string RequestedBy { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
