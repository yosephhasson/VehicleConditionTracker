using VehicleConditionTracker.Domain.Enums;

namespace VehicleConditionTracker.Application.Dtos.Reports;

public record VehicleReportDto(
    Guid Id,
    string Vin,
    int Year,
    string Make,
    string Model,
    int Mileage,
    string Color,
    string? InspectorNotes,
    ReportStatus Status,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc);
