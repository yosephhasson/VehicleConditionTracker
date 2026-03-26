using VehicleConditionTracker.Domain.Common;
using VehicleConditionTracker.Domain.Enums;

namespace VehicleConditionTracker.Domain.Entities;

public class VehicleFinding : BaseEntity
{
    public Guid VehicleSectionId { get; set; }
    public VehicleSection VehicleSection { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public FindingSeverity Severity { get; set; }
    public bool IsResolved { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}
