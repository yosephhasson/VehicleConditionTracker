using VehicleConditionTracker.Domain.Enums;

namespace VehicleConditionTracker.Application.Dtos.Reports;

public record UpdateReportStatusRequest(ReportStatus Status);
