using VehicleConditionTracker.Domain.Common;

namespace VehicleConditionTracker.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public ICollection<VehicleReport> Reports { get; set; } = new List<VehicleReport>();
}
