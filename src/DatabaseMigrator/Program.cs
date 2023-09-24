using CommandLine;
using Serilog;
using TRS.FinalPlantasy.DatabaseMigrator.Application;
using TRS.FinalPlantasy.DatabaseMigrator.Infrastructure;
using TRS.FinalPlantasy.DatabaseMigrator.Options;

Log.Logger = StartupLoggerCreator.Create();

// Will be replaced during application construction so get ahold of it now
var startupLogger = Log.Logger;

try
{
    startupLogger.Information("Starting up!");

    Parser.Default.ParseArguments<CommandLineOptions>(args)
        .WithParsed(
            async (options) => 
            { 
                await RunApplicationAsync(options, startupLogger);  
            });

    return 0;
}
catch (Exception e)
{
    startupLogger.Fatal(e, "Startup failed");

    return 1;
}
finally
{
    startupLogger.Information("Closing");

    Log.CloseAndFlush();
}

static async Task RunApplicationAsync(CommandLineOptions commandLineOptions, Serilog.ILogger logger)
{
    var cts = ConfigureCancellation(logger);

    logger.Information("Running Application - Press CTRL+C to end");

    var options = MigratorOptionsReader.Read(commandLineOptions);
    var migrator = new DatabaseMigrationRunner();

    await migrator.RunAsync(options, cts.Token);
}

static CancellationTokenSource ConfigureCancellation(Serilog.ILogger logger)
{
    var cts = new CancellationTokenSource();

    Console.CancelKeyPress += (s, e) =>
    {
        logger.Information("Cancelling...");

        cts.Cancel();

        e.Cancel = true;
    };

    return cts;
}
