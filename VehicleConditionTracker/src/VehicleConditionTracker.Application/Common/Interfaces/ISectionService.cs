using VehicleConditionTracker.Application.Dtos.Sections;

namespace VehicleConditionTracker.Application.Common.Interfaces;

public interface ISectionService
{
    Task<bool> CreateAsync(Guid reportId, CreateSectionRequest request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid sectionId, UpdateSectionRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid sectionId, CancellationToken cancellationToken = default);
}
