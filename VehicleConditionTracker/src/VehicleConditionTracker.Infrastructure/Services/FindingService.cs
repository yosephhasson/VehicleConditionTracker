using Microsoft.EntityFrameworkCore;
using VehicleConditionTracker.Application.Common.Exceptions;
using VehicleConditionTracker.Application.Common.Interfaces;
using VehicleConditionTracker.Application.Dtos.Findings;
using VehicleConditionTracker.Domain.Entities;
using VehicleConditionTracker.Infrastructure.Persistence;

namespace VehicleConditionTracker.Infrastructure.Services;

public class FindingService : IFindingService
{
    private readonly AppDbContext _dbContext;
    private readonly ICurrentUserService _currentUser;

    public FindingService(AppDbContext dbContext, ICurrentUserService currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<bool> CreateAsync(Guid sectionId, CreateFindingRequest request, CancellationToken cancellationToken = default)
    {
        var userId = _currentUser.UserId ?? throw new UnauthorizedException("User context missing.");
        var sectionExists = await _dbContext.VehicleSections
            .Include(s => s.VehicleReport)
            .AnyAsync(s => s.Id == sectionId && s.VehicleReport.UserId == userId, cancellationToken);
        if (!sectionExists) return false;

        var entity = new VehicleFinding
        {
            Id = Guid.NewGuid(),
            VehicleSectionId = sectionId,
            Title = request.Title,
            Description = request.Description,
            Severity = request.Severity,
            IsResolved = request.IsResolved,
            CreatedAtUtc = DateTime.UtcNow,
            UpdatedAtUtc = DateTime.UtcNow
        };

        _dbContext.VehicleFindings.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> UpdateAsync(Guid findingId, UpdateFindingRequest request, CancellationToken cancellationToken = default)
    {
        var userId = _currentUser.UserId ?? throw new UnauthorizedException("User context missing.");
        var entity = await _dbContext.VehicleFindings
            .Include(f => f.VehicleSection)
            .ThenInclude(s => s.VehicleReport)
            .SingleOrDefaultAsync(f => f.Id == findingId && f.VehicleSection.VehicleReport.UserId == userId, cancellationToken);
        if (entity is null) return false;

        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.Severity = request.Severity;
        entity.IsResolved = request.IsResolved;
        entity.UpdatedAtUtc = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid findingId, CancellationToken cancellationToken = default)
    {
        var userId = _currentUser.UserId ?? throw new UnauthorizedException("User context missing.");
        var entity = await _dbContext.VehicleFindings
            .Include(f => f.VehicleSection)
            .ThenInclude(s => s.VehicleReport)
            .SingleOrDefaultAsync(f => f.Id == findingId && f.VehicleSection.VehicleReport.UserId == userId, cancellationToken);
        if (entity is null) return false;

        _dbContext.VehicleFindings.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
