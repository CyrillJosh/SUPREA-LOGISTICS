namespace SUPREA_LOGISTICS.Models
{
    public class Driver
    {
        public int DriverId { get; set; }                 
        public string FullName { get; set; } = null!;     
        public string? Classification { get; set; }      
        public string? Position { get; set; }            
        public bool IsActive { get; set; } = true;
        public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
