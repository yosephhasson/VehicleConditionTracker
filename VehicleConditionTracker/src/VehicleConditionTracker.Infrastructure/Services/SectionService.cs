using Microsoft.EntityFrameworkCore;
using VehicleConditionTracker.Application.Common.Exceptions;
using VehicleConditionTracker.Application.Common.Interfaces;
using VehicleConditionTracker.Application.Dtos.Sections;
using VehicleConditionTracker.Domain.Entities;
using VehicleConditionTracker.Infrastructure.Persistence;

namespace VehicleConditionTracker.Infrastructure.Services;

public class SectionService : ISectionService
{
    private readonly AppDbContext _dbContext;
    private readonly ICurrentUserService _currentUser;

    public SectionService(AppDbContext dbContext, ICurrentUserService currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<bool> CreateAsync(Guid reportId, CreateSectionRequest request, CancellationToken cancellationToken = default)
    {
        var userId = _currentUser.UserId ?? throw new UnauthorizedException("User context missing.");
        var exists = await _dbContext.VehicleReports.AnyAsync(r => r.Id == reportId && r.UserId == userId, cancellationToken);
        if (!exists) return false;

        var entity = new VehicleSection
        {
            Id = Guid.NewGuid(),
            VehicleReportId = reportId,
            SectionType = request.SectionType,
            Title = request.Title,
            Notes = request.Notes,
            SortOrder = request.SortOrder
        };

        _dbContext.VehicleSections.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> UpdateAsync(Guid sectionId, UpdateSectionRequest request, CancellationToken cancellationToken = default)
    {
        var userId = _currentUser.UserId ?? throw new UnauthorizedException("User context missing.");
        var entity = await _dbContext.VehicleSections
            .Include(s => s.VehicleReport)
            .SingleOrDefaultAsync(s => s.Id == sectionId && s.VehicleReport.UserId == userId, cancellationToken);
        if (entity is null) return false;

        entity.SectionType = request.SectionType;
        entity.Title = request.Title;
        entity.Notes = request.Notes;
        entity.SortOrder = request.SortOrder;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid sectionId, CancellationToken cancellationToken = default)
    {
        var userId = _currentUser.UserId ?? throw new UnauthorizedException("User context missing.");
        var entity = await _dbContext.VehicleSections
            .Include(s => s.VehicleReport)
            .SingleOrDefaultAsync(s => s.Id == sectionId && s.VehicleReport.UserId == userId, cancellationToken);
        if (entity is null) return false;

        _dbContext.VehicleSections.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
