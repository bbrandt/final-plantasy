using Microsoft.Extensions.DependencyInjection;
using TRS.FinalPlantasy.Infrastructure.EntityFramework.Extensions;
using TRS.FinalPlantasy.Infrastructure.EntityFramework.Planning;
using TRS.FinalPlantasy.Infrastructure.Sqlite.Planning;

namespace TRS.FinalPlantasy.Infrastructure.Sqlite.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddSqlitePersistence(this IServiceCollection services)
    {
        services.AddPlanningPersistence();

        services.AddTransient<IPlanningContextConfiguration, PlanningContextConfiguration>();

        return services;
    }
}
