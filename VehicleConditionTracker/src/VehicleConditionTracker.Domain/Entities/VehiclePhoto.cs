using VehicleConditionTracker.Domain.Common;

namespace VehicleConditionTracker.Domain.Entities;

public class VehiclePhoto : BaseEntity
{
    public Guid? VehicleReportId { get; set; }
    public VehicleReport? VehicleReport { get; set; }

    public Guid? VehicleSectionId { get; set; }
    public VehicleSection? VehicleSection { get; set; }

    public string OriginalFileName { get; set; } = null!;
    public string StoredFileName { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public long FileSizeBytes { get; set; }
    public string StoragePath { get; set; } = null!;
    public DateTime UploadedAtUtc { get; set; }
}
