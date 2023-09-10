using Microsoft.Extensions.DependencyInjection;
using TRS.FinalPlantasy.Domain.Abstractions.Planning;
using TRS.FinalPlantasy.Domain.Planning;
using TRS.FinalPlantasy.Domain.Planning.Timelines;

namespace TRS.FinalPlantasy.Domain.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddPlanningDomain(this IServiceCollection services)
    {
        services.AddTransient<IPlanEntryDomainService, PlanEntryDomainService>();
        services.AddTransient<IPlanTimelineDomainService, PlanTimelineDomainService>();
        services.AddTransient<ExpandedEventCalculator>();
        services.AddTransient<PlanBalanceCalculator>();

        return services;
    }
}
