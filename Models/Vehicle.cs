using System;
using System.Collections.Generic;

namespace SUPREA_LOGISTICS.Models;

public partial class Vehicle
{
    public int VehicleId { get; set; }

    public string? UnitType { get; set; }

    public string? UnitModelSeries { get; set; }

    public string? BrandMake { get; set; }

    public int? YearModel { get; set; }

    public string? EngineNo { get; set; }

    public string? ChassisNo { get; set; }

    public string? PlateNo { get; set; }

    public bool? Orcr { get; set; }

    public DateOnly? ExpirationDate { get; set; }

    public string? Insurance { get; set; }

    public string? InsuranceCoverage { get; set; }

    public string? InsuranceProvider { get; set; }

    public DateOnly? DateAcquired { get; set; }

    public string? Supplier { get; set; }

    public string? ProjectId { get; set; }

    public string? SiteLocation { get; set; }

    public string? VehicleStatus { get; set; }

    public virtual ICollection<MaintenanceLog> MaintenanceLogs { get; set; } = new List<MaintenanceLog>();

    public virtual ICollection<VehicleDocument> VehicleDocuments { get; set; } = new List<VehicleDocument>();

    public virtual ICollection<VehicleLog> VehicleLogs { get; set; } = new List<VehicleLog>();

    public virtual ICollection<VehiclePicture> VehiclePictures { get; set; } = new List<VehiclePicture>();
}
