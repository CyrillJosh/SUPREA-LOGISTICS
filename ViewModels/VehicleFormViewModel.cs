using SUPREA_LOGISTICS.Models;

namespace SUPREA_LOGISTICS.ViewModels
{
    public class VehicleFormViewModel
    {
        public Vehicle Vehicle { get; set; } = new Vehicle();

        public List<IFormFile>? VehiclePictures { get; set; }

        public List<IFormFile>? VehicleDocuments { get; set; }

        public List<string>? DocumentTypes { get; set; } 
        public List<DateTime?>? DocumentExpirationDates { get; set; } 
    }
}
