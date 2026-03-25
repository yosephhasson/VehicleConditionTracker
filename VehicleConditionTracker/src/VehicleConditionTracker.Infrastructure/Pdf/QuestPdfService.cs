using VehicleConditionTracker.Application.Common.Interfaces;

namespace VehicleConditionTracker.Infrastructure.Pdf;

public class QuestPdfService : IPdfService
{
    public Task<byte[]> GenerateReportPdfAsync(Guid reportId, CancellationToken cancellationToken = default)
    {
        // TODO: implement QuestPDF rendering
        return Task.FromResult(Array.Empty<byte>());
    }
}
