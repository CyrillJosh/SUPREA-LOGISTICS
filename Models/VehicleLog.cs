using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace SUPREA_LOGISTICS.Models;

public partial class VehicleLog
{
    public int LogId { get; set; }

    public int VehicleId { get; set; }

    public DateOnly LogDate { get; set; }

    public string? DriverName { get; set; }

    public string? Purpose { get; set; }

    public DateTime? CreatedAt { get; set; }
    [ValidateNever]
    public virtual Vehicle Vehicle { get; set; } = null!;
}
