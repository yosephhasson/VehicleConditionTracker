using VehicleConditionTracker.Domain.Enums;

namespace VehicleConditionTracker.Application.Dtos.Findings;

public record CreateFindingRequest(string Title, string Description, FindingSeverity Severity, bool IsResolved);
