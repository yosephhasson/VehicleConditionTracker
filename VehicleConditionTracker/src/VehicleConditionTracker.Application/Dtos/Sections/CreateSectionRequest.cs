using VehicleConditionTracker.Domain.Enums;

namespace VehicleConditionTracker.Application.Dtos.Sections;

public record CreateSectionRequest(SectionType SectionType, string Title, string? Notes, int SortOrder);
