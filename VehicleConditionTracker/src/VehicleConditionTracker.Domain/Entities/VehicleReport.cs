using VehicleConditionTracker.Domain.Common;
using VehicleConditionTracker.Domain.Enums;

namespace VehicleConditionTracker.Domain.Entities;

public class VehicleReport : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public string Vin { get; set; } = null!;
    public int Year { get; set; }
    public string Make { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int Mileage { get; set; }
    public string Color { get; set; } = null!;
    public string? InspectorNotes { get; set; }
    public ReportStatus Status { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }

    public ICollection<VehicleSection> Sections { get; set; } = new List<VehicleSection>();
    public ICollection<VehiclePhoto> Photos { get; set; } = new List<VehiclePhoto>();
}
