using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TRS.FinalPlantasy.DatabaseMigrator.Application;
using TRS.FinalPlantasy.DatabaseMigrator.Options;

namespace TRS.FinalPlantasy.DatabaseMigrator.Extensions;

internal static class ServicesExtensions
{
    public static IServiceCollection AddMigrationApplication(
        this IServiceCollection services, 
        MigratorDatabaseType databaseType)
    {
        switch (databaseType)
        {
            case MigratorDatabaseType.SqlServer:
                services.AddTransient<IDatabaseCreator, SqlServerDatabaseCreator>();
                break;
            case MigratorDatabaseType.Sqlite:
                services.AddTransient<IDatabaseCreator, SqliteDatabaseCreator>();
                break;
            default:
                throw new Exception("The database type is unknown");
        }

        return services;
    }
}
