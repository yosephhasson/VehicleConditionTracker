using Microsoft.Extensions.FileProviders;

namespace VehicleConditionTracker.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseUploadStaticFiles(this IApplicationBuilder app, IConfiguration configuration)
    {
        var root = configuration.GetSection("FileStorage")["RootPath"] ?? "Uploads";
        var physicalPath = Path.Combine(Directory.GetCurrentDirectory(), root);
        Directory.CreateDirectory(physicalPath);

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(physicalPath),
            RequestPath = "/uploads"
        });

        return app;
    }
}
