using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace SUPREA_LOGISTICS.Models;

public partial class MaintenanceLog
{
    public int MaintenanceId { get; set; }

    public int VehicleId { get; set; }

    public DateOnly MaintenanceDate { get; set; }

    public string? MaintenanceType { get; set; }

    public string? Description { get; set; }

    public string? ServiceProvider { get; set; }

    public decimal? Cost { get; set; }

    public DateTime? CreatedAt { get; set; }
    public string? Remarks { get; set; }
    
    [ValidateNever]
    public virtual Vehicle Vehicle { get; set; } = null!;
}
