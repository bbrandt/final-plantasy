using FluentMigrator.Runner;
using FluentMigrator.Runner.Processors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TRS.FinalPlantasy.DatabaseMigrator.Extensions;
using TRS.FinalPlantasy.DatabaseMigrator.Options;

namespace TRS.FinalPlantasy.DatabaseMigrator.Application;

public class DatabaseMigrationRunner
{
    public async Task RunAsync(MigratorOptions options, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(options.ConnectionString))
        {
            throw new NullReferenceException("Connection string is required for migrations");
        }

        var services = BuildServiceProvider(options);

        await CreateDatabaseAsync(
            services, 
            options.ConnectionString, 
            cancellationToken);

        RunMigrations(services);
    }

    private static async Task CreateDatabaseAsync(
        IServiceProvider services, 
        string connectionString, 
        CancellationToken cancellationToken)
    {
        var creator = services.GetRequiredService<IDatabaseCreator>();

        await creator.CreateAsync(connectionString, cancellationToken);
    }

    private static void RunMigrations(IServiceProvider services)
    {
        var runner = services.GetRequiredService<IMigrationRunner>();

        runner.MigrateUp();
    }

    private static IServiceProvider BuildServiceProvider(MigratorOptions options)
    {
        var collection = new ServiceCollection();

        collection
            .AddMigrationApplication(options.DatabaseType)
            .AddFluentMigratorCore();

        collection
            .ConfigureRunner(configure => 
            {
                configure.WithGlobalConnectionString(options.ConnectionString);

                configure.ScanIn(typeof(DatabaseMigrationRunner).Assembly)
                    .For
                    .Migrations();

                switch (options.DatabaseType)
                {
                    case MigratorDatabaseType.SqlServer:
                        configure.AddSqlServer2016();
                        break;
                    case MigratorDatabaseType.Sqlite:
                        configure.AddSQLite();
                        break;
                    default:
                        throw new Exception("The database type is unknown");
                }
            })
            .AddLogging(configure => configure.AddFluentMigratorConsole().SetMinimumLevel(LogLevel.Information));

        var serviceProvider = collection.BuildServiceProvider();

        return serviceProvider;
    }
}
