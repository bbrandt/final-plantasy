using Microsoft.Extensions.DependencyInjection;
using TRS.FinalPlantasy.Infrastructure.EntityFramework.Extensions;
using TRS.FinalPlantasy.Infrastructure.EntityFramework.Planning;
using TRS.FinalPlantasy.Infrastructure.SqlServer.Planning;

namespace TRS.FinalPlantasy.Infrastructure.SqlServer.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddSqlServerPersistence(this IServiceCollection services)
    {
        services.AddPlanningPersistence();

        services.AddTransient<IPlanningContextConfiguration, PlanningContextConfiguration>();

        return services;
    }
}
