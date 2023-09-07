using Microsoft.Extensions.DependencyInjection;
using TRS.FinalPlantasy.Domain.Planning;

namespace TRS.FinalPlantasy.Domain.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddPlanningDomain(this IServiceCollection services)
    {
        services.AddTransient<PlanningDomainService>();

        return services;
    }
}
