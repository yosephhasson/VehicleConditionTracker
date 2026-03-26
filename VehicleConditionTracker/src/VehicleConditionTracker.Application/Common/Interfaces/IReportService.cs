using VehicleConditionTracker.Application.Dtos.Reports;
using VehicleConditionTracker.Domain.Enums;

namespace VehicleConditionTracker.Application.Common.Interfaces;

public interface IReportService
{
    Task<IEnumerable<VehicleReportDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<VehicleReportDto?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<VehicleReportDto> CreateAsync(CreateVehicleReportRequest request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id, UpdateVehicleReportRequest request, CancellationToken cancellationToken = default);
    Task<bool> UpdateStatusAsync(Guid id, ReportStatus status, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
