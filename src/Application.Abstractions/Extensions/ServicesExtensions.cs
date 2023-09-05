using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TRS.FinalPlantasy.Application.Abstractions.Options;

namespace TRS.FinalPlantasy.Application.Abstractions.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddApplicationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PlanningOptions>(
            configuration.GetRequiredSection(nameof(PlanningOptions))
        );

        return services;
    }
}
