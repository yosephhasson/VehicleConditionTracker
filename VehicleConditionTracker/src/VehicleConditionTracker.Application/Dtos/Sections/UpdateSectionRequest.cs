using VehicleConditionTracker.Domain.Enums;

namespace VehicleConditionTracker.Application.Dtos.Sections;

public record UpdateSectionRequest(SectionType SectionType, string Title, string? Notes, int SortOrder);
