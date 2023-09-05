using Microsoft.Extensions.DependencyInjection;
using TRS.FinalPlantasy.Infrastructure.SqlServer.Planning;

namespace TRS.FinalPlantasy.Infrastructure.SqlServer.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddSqlServerPersistence(this IServiceCollection services)
    {
        services.AddTransient<PlanContextConfiguration>();
        services.AddTransient<PlanContextModelBuilder>();

        services.AddDbContext<PlanContext>(ServiceLifetime.Scoped);

        return services;
    }
}
