namespace SUPREA_LOGISTICS.Models
{
    public class VehicleLog
    {
        public int LogId { get; set; }
        public string VehicleId { get; set; }   // Links to Vehicle.VehicleId

        public DateTime Date { get; set; }
        public string DriverName { get; set; }
        public string VehiclePlateNo { get; set; }

        public int StartOdometer { get; set; }
        public int EndOdometer { get; set; }

        public string PurposeOfTrip { get; set; }

        public int DistanceTraveled => EndOdometer - StartOdometer;
    }
}
