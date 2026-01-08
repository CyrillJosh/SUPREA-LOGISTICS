using System;
using System.Collections.Generic;

namespace SUPREA_LOGISTICS.Models;

public partial class VehiclePicture
{
    public int PictureId { get; set; }

    public int VehicleId { get; set; }

    public string FileName { get; set; } = null!;

    public string? FileType { get; set; }

    public byte[] FileData { get; set; } = null!;

    public DateTime? UploadedDate { get; set; }

    public virtual Vehicle Vehicle { get; set; } = null!;
}
