using Microsoft.EntityFrameworkCore;
using VehicleConditionTracker.Application.Common.Interfaces;
using VehicleConditionTracker.Application.Dtos.Findings;
using VehicleConditionTracker.Domain.Entities;
using VehicleConditionTracker.Infrastructure.Persistence;

namespace VehicleConditionTracker.Infrastructure.Services;

public class FindingService : IFindingService
{
    private readonly AppDbContext _dbContext;

    public FindingService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CreateAsync(Guid sectionId, CreateFindingRequest request, CancellationToken cancellationToken = default)
    {
        var sectionExists = await _dbContext.VehicleSections.AnyAsync(s => s.Id == sectionId, cancellationToken);
        if (!sectionExists) return false;

        var entity = new VehicleFinding
        {
            Id = Guid.NewGuid(),
            VehicleSectionId = sectionId,
            Title = request.Title,
            Description = request.Description,
            Severity = request.Severity,
            IsResolved = request.IsResolved
        };

        _dbContext.VehicleFindings.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> UpdateAsync(Guid findingId, UpdateFindingRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.VehicleFindings.SingleOrDefaultAsync(f => f.Id == findingId, cancellationToken);
        if (entity is null) return false;

        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.Severity = request.Severity;
        entity.IsResolved = request.IsResolved;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid findingId, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.VehicleFindings.SingleOrDefaultAsync(f => f.Id == findingId, cancellationToken);
        if (entity is null) return false;

        _dbContext.VehicleFindings.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
