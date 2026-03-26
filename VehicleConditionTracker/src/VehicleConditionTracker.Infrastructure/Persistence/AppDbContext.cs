using Microsoft.EntityFrameworkCore;
using VehicleConditionTracker.Domain.Entities;
using VehicleConditionTracker.Domain.Enums;

namespace VehicleConditionTracker.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<VehicleReport> VehicleReports => Set<VehicleReport>();
    public DbSet<VehicleSection> VehicleSections => Set<VehicleSection>();
    public DbSet<VehicleFinding> VehicleFindings => Set<VehicleFinding>();
    public DbSet<VehiclePhoto> VehiclePhotos => Set<VehiclePhoto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Email).IsRequired().HasMaxLength(256);
            b.Property(x => x.PasswordHash).IsRequired();
            b.HasIndex(x => x.Email).IsUnique();
        });

        modelBuilder.Entity<VehicleReport>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Vin).IsRequired().HasMaxLength(32);
            b.Property(x => x.Make).IsRequired().HasMaxLength(128);
            b.Property(x => x.Model).IsRequired().HasMaxLength(128);
            b.Property(x => x.Color).IsRequired().HasMaxLength(64);
            b.Property(x => x.Status).HasDefaultValue(ReportStatus.Draft);
            b.HasOne(x => x.User)
                .WithMany(u => u.Reports)
                .HasForeignKey(x => x.UserId);
            b.HasMany(x => x.Sections)
                .WithOne(s => s.VehicleReport)
                .HasForeignKey(s => s.VehicleReportId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasMany(x => x.Photos)
                .WithOne(p => p.VehicleReport)
                .HasForeignKey(p => p.VehicleReportId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<VehicleSection>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Title).IsRequired().HasMaxLength(128);
            b.Property(x => x.SortOrder).HasDefaultValue(0);
            b.HasMany(x => x.Findings)
                .WithOne(f => f.VehicleSection)
                .HasForeignKey(f => f.VehicleSectionId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasMany(x => x.Photos)
                .WithOne(p => p.VehicleSection)
                .HasForeignKey(p => p.VehicleSectionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<VehicleFinding>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Title).IsRequired().HasMaxLength(128);
            b.Property(x => x.Description).IsRequired().HasMaxLength(2048);
        });

        modelBuilder.Entity<VehiclePhoto>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.OriginalFileName).IsRequired();
            b.Property(x => x.StoredFileName).IsRequired();
            b.Property(x => x.StoragePath).IsRequired();
        });
    }
}
