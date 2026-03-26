using Microsoft.EntityFrameworkCore;
using VehicleConditionTracker.Application.Common.Exceptions;
using VehicleConditionTracker.Application.Common.Interfaces;
using VehicleConditionTracker.Application.Dtos.Reports;
using VehicleConditionTracker.Domain.Entities;
using VehicleConditionTracker.Domain.Enums;
using VehicleConditionTracker.Infrastructure.Persistence;

namespace VehicleConditionTracker.Infrastructure.Services;

public class ReportService : IReportService
{
    private readonly AppDbContext _dbContext;

    private readonly ICurrentUserService _currentUser;

    public ReportService(AppDbContext dbContext, ICurrentUserService currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<IEnumerable<VehicleReportDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var userId = _currentUser.UserId ?? throw new UnauthorizedException("User context missing.");
        return await _dbContext.VehicleReports
            .AsNoTracking()
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.CreatedAtUtc)
            .Select(r => new VehicleReportDto(r.Id, r.Vin, r.Year, r.Make, r.Model, r.Mileage, r.Color, r.InspectorNotes, r.Status, r.CreatedAtUtc, r.UpdatedAtUtc))
            .ToListAsync(cancellationToken);
    }

    public async Task<VehicleReportDto?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var userId = _currentUser.UserId ?? throw new UnauthorizedException("User context missing.");
        return await _dbContext.VehicleReports
            .AsNoTracking()
            .Where(r => r.Id == id && r.UserId == userId)
            .Select(r => new VehicleReportDto(r.Id, r.Vin, r.Year, r.Make, r.Model, r.Mileage, r.Color, r.InspectorNotes, r.Status, r.CreatedAtUtc, r.UpdatedAtUtc))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<VehicleReportDto> CreateAsync(CreateVehicleReportRequest request, CancellationToken cancellationToken = default)
    {
        var userId = _currentUser.UserId ?? throw new UnauthorizedException("User context missing.");

        var now = DateTime.UtcNow;
        var entity = new VehicleReport
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Vin = request.Vin,
            Year = request.Year,
            Make = request.Make,
            Model = request.Model,
            Mileage = request.Mileage,
            Color = request.Color,
            InspectorNotes = request.InspectorNotes,
            Status = request.Status,
            CreatedAtUtc = now,
            UpdatedAtUtc = now
        };

        _dbContext.VehicleReports.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new VehicleReportDto(entity.Id, entity.Vin, entity.Year, entity.Make, entity.Model, entity.Mileage, entity.Color, entity.InspectorNotes, entity.Status, entity.CreatedAtUtc, entity.UpdatedAtUtc);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateVehicleReportRequest request, CancellationToken cancellationToken = default)
    {
        var userId = _currentUser.UserId ?? throw new UnauthorizedException("User context missing.");
        var entity = await _dbContext.VehicleReports.SingleOrDefaultAsync(r => r.Id == id && r.UserId == userId, cancellationToken);
        if (entity is null) return false;

        entity.Vin = request.Vin;
        entity.Year = request.Year;
        entity.Make = request.Make;
        entity.Model = request.Model;
        entity.Mileage = request.Mileage;
        entity.Color = request.Color;
        entity.InspectorNotes = request.InspectorNotes;
        entity.Status = request.Status;
        entity.UpdatedAtUtc = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> UpdateStatusAsync(Guid id, ReportStatus status, CancellationToken cancellationToken = default)
    {
        var userId = _currentUser.UserId ?? throw new UnauthorizedAccessException("User context missing.");
        var entity = await _dbContext.VehicleReports.SingleOrDefaultAsync(r => r.Id == id && r.UserId == userId, cancellationToken);
        if (entity is null) return false;
        entity.Status = status;
        entity.UpdatedAtUtc = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var userId = _currentUser.UserId ?? throw new UnauthorizedAccessException("User context missing.");
        var entity = await _dbContext.VehicleReports
            .Include(r => r.Sections)
            .Include(r => r.Photos) // ensure photos are tracked for cascade awareness
            .SingleOrDefaultAsync(r => r.Id == id && r.UserId == userId, cancellationToken);
        if (entity is null) return false;
        _dbContext.VehicleReports.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
