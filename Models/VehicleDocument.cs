using System;
using System.Collections.Generic;

namespace SUPREA_LOGISTICS.Models;

public partial class VehicleDocument
{
    public int DocumentId { get; set; }

    public int VehicleId { get; set; }

    public string? DocumentType { get; set; }

    public string? DocumentNumber { get; set; }

    public string FileName { get; set; } = null!;

    public string? FileType { get; set; }

    public byte[] FileData { get; set; } = null!;

    public DateOnly? IssueDate { get; set; }

    public DateOnly? ExpirationDate { get; set; }

    public DateTime? UploadedDate { get; set; }

    public bool IsAvailable { get; set; }

    public virtual Vehicle Vehicle { get; set; } = null!;
}
