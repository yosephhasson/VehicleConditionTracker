using Microsoft.Extensions.Configuration;
using VehicleConditionTracker.Application.Common.Interfaces;

namespace VehicleConditionTracker.Infrastructure.Files;

public class LocalFileStorage : IFileStorage
{
    private readonly string _root;

    public LocalFileStorage(IConfiguration configuration)
    {
        _root = configuration.GetSection("FileStorage")["RootPath"] ?? "Uploads";
        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), _root));
    }

    public async Task<string> SaveAsync(Stream stream, string fileName, string contentType, CancellationToken cancellationToken = default)
    {
        var storedName = $"{Guid.NewGuid()}_{fileName}";
        var path = Path.Combine(_root, storedName);
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), path);

        await using var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        await stream.CopyToAsync(fileStream, cancellationToken);

        return path.Replace("\\", "/");
    }

    public Task DeleteAsync(string storagePath, CancellationToken cancellationToken = default)
    {
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), storagePath);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        return Task.CompletedTask;
    }
}
