using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace SUPREA_LOGISTICS.Models;

public partial class VehicleLog
{
    public int LogId { get; set; }

    public int VehicleId { get; set; }

    public string? Description { get; set; }

    public string? Remarks { get; set; }
    public DateTime? CreatedAt { get; set; }
    [ValidateNever]
    public virtual Vehicle Vehicle { get; set; } = null!;
}
