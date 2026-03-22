using VehicleConditionTracker.Domain.Common;
using VehicleConditionTracker.Domain.Enums;

namespace VehicleConditionTracker.Domain.Entities;

public class VehicleSection : BaseEntity
{
    public Guid VehicleReportId { get; set; }
    public VehicleReport VehicleReport { get; set; } = null!;
    public SectionType SectionType { get; set; }
    public string Title { get; set; } = null!;
    public string? Notes { get; set; }
    public int SortOrder { get; set; }

    public ICollection<VehicleFinding> Findings { get; set; } = new List<VehicleFinding>();
    public ICollection<VehiclePhoto> Photos { get; set; } = new List<VehiclePhoto>();
}
