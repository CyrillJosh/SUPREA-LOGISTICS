namespace SUPREA_LOGISTICS.Models
{
    public class MaintenanceLog
    {
        public int MaintenanceId { get; set; }
        public string VehicleId { get; set; }   // Links to Vehicle.VehicleId

        public DateTime DateIssued { get; set; }
        public string MaintenanceType { get; set; }
        public string WorkPerformed { get; set; }
        public string Supplier { get; set; }
        public bool PartsReplaced { get; set; }
        public DateTime DateDone { get; set; }
    }
}
