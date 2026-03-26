using VehicleConditionTracker.Application.Dtos.Findings;

namespace VehicleConditionTracker.Application.Common.Interfaces;

public interface IFindingService
{
    Task<bool> CreateAsync(Guid sectionId, CreateFindingRequest request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid findingId, UpdateFindingRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid findingId, CancellationToken cancellationToken = default);
}
