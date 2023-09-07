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

        var services = BuildServiceProvider(options.ConnectionString);

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
        var creator = services.GetRequiredService<DatabaseCreator>();

        await creator.CreateAsync(connectionString, cancellationToken);
    }

    private static void RunMigrations(IServiceProvider services)
    {
        var runner = services.GetRequiredService<IMigrationRunner>();

        runner.MigrateUp();
    }

    private static IServiceProvider BuildServiceProvider(string connectionString)
    {
        var collection = new ServiceCollection();

        collection
            .AddMigrationApplication()
            .AddFluentMigratorCore()
            .ConfigureRunner(configure => 
            {
                configure.ScanIn(typeof(DatabaseMigrationRunner).Assembly)
                    .For
                    .Migrations();

                configure.AddSqlServer2016();
            })
            .AddLogging(configure => configure.AddFluentMigratorConsole().SetMinimumLevel(LogLevel.Information))
            .Configure<ProcessorOptions>(opt => 
            {
                opt.ConnectionString = connectionString;
            });

        var serviceProvider = collection.BuildServiceProvider();

        return serviceProvider;
    }
}
