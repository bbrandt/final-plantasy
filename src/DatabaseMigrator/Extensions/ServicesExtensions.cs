using Microsoft.Extensions.DependencyInjection;
using TRS.FinalPlantasy.DatabaseMigrator.Application;

namespace TRS.FinalPlantasy.DatabaseMigrator.Extensions;

internal static class ServicesExtensions
{
    public static IServiceCollection AddMigrationApplication(this IServiceCollection services)
    {
        services.AddScoped<DatabaseMigrationRunner>();
        services.AddScoped<DatabaseCreator>();

        return services;
    }
}
