using Microsoft.Extensions.DependencyInjection;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Repositories;
using TRS.FinalPlantasy.Infrastructure.SqlServer.Common;
using TRS.FinalPlantasy.Infrastructure.SqlServer.Planning;
using TRS.FinalPlantasy.Infrastructure.SqlServer.Planning.Repositories;

namespace TRS.FinalPlantasy.Infrastructure.SqlServer.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddSqlServerPersistence(this IServiceCollection services)
    {
        services.AddPlanning();

        return services;
    }

    private static IServiceCollection AddPlanning(this IServiceCollection services)
    {
        services.AddTransient<PlanningContextConfiguration>();
        services.AddTransient<PlanningContextModelBuilder>();
        services.AddDbContext<PlanningContext>(ServiceLifetime.Scoped);

        services.AddTransient<IPlanEntryRepository, PlanEntryRepository>();
        services.AddTransient<IPlanEntryUnitOfWorkFactory, PlanEntryUnitOfWorkFactory>();
        services.AddTransient<IUnitOfWorkCreator<IPlanEntryUnitOfWork>, PlanEntryUnitOfWorkCreator>();

        return services;
    }
}
