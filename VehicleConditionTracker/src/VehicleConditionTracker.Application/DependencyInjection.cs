using Microsoft.Extensions.DependencyInjection;

namespace VehicleConditionTracker.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Add validators, mediators, mappers here later
        return services;
    }
}
