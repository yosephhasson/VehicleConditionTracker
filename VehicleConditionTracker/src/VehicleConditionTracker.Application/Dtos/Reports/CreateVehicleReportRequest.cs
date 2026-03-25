using VehicleConditionTracker.Domain.Enums;

namespace VehicleConditionTracker.Application.Dtos.Reports;

public record CreateVehicleReportRequest(
    string Vin,
    int Year,
    string Make,
    string Model,
    int Mileage,
    string Color,
    string? InspectorNotes,
    ReportStatus Status);
