using SUPREA_LOGISTICS.Models;

namespace SUPREA_LOGISTICS.ViewModels
{
    public class VehicleDetailsViewModel
    {
        public Vehicle Vehicle { get; set; }
        public List<MaintenanceLog> MaintenanceLogs { get; set; }
        public List<VehicleLog> VehicleLogs { get; set; }
        public List<Document> Documents { get; set; }
        public List<Picture> Pictures { get; set; }
    }
}
