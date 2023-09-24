using Microsoft.Extensions.DependencyInjection;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Queries;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Repositories;
using TRS.FinalPlantasy.Infrastructure.EntityFramework.Common;
using TRS.FinalPlantasy.Infrastructure.EntityFramework.Planning;
using TRS.FinalPlantasy.Infrastructure.EntityFramework.Planning.Queries;
using TRS.FinalPlantasy.Infrastructure.EntityFramework.Planning.Repositories;

namespace TRS.FinalPlantasy.Infrastructure.EntityFramework.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddPlanningPersistence(this IServiceCollection services)
    {
        services.AddTransient<PlanningContextModelBuilder>();
        services.AddDbContext<PlanningContext>(ServiceLifetime.Scoped);

        services.AddTransient<IPlanningQueryContext, PlanningQueryContext>();
        services.AddTransient<IPlanEntryRepository, PlanEntryRepository>();
        services.AddTransient<IPlanEntryUnitOfWorkFactory, PlanEntryUnitOfWorkFactory>();
        services.AddTransient<IUnitOfWorkCreator<IPlanEntryUnitOfWork>, PlanEntryUnitOfWorkCreator>();

        return services;
    }
}
