namespace VehicleConditionTracker.Application.Common.Interfaces;

public interface IPdfService
{
    Task<byte[]> GenerateReportPdfAsync(Guid reportId, CancellationToken cancellationToken = default);
}
