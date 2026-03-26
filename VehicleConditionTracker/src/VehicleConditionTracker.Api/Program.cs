using VehicleConditionTracker.Application;
using VehicleConditionTracker.Infrastructure;
using VehicleConditionTracker.Api.Middleware;
using VehicleConditionTracker.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

// Serve uploaded files
app.UseUploadStaticFiles(builder.Configuration);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
