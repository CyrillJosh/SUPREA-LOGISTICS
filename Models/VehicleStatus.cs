namespace SUPREA_LOGISTICS.Models
{
    public class VehicleStatus
    {
        public int VehicleId { get; set; }
        public DateTime StatusDate { get; set; }
        public int VehicleStatusId { get; set; }
        public string Status { get; set; }
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}
