using Serilog;
using TRS.FinalPlantasy.Application.Abstractions.Infrastructure;
using TRS.FinalPlantasy.Web.Infrastructure;

Log.Logger = StartupLoggerCreator.Create();

// Will be replaced during application construction so get ahold of it now
var startupLogger = Log.Logger;

try
{
    startupLogger.Information("Starting up!");

    var builder = MainWebApplicationBuilderCreator.Create(args, startupLogger);

    var app = MainWebApplicationBuilder.Build(builder, startupLogger);

    await RunApplicationAsync(app, startupLogger);

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

static async Task RunApplicationAsync(WebApplication app, Serilog.ILogger logger)
{
    LogApplicationId(app.Services, logger);

    var cts = ConfigureCancellation(logger);

    logger.Information("Running Application - Press CTRL+C to end");

    await app.RunAsync(cts.Token);
}

static void LogApplicationId(IServiceProvider services, Serilog.ILogger logger)
{
    var appIdProvider = services.GetRequiredService<IApplicationIdProvider>();

    logger.Information("Running with application id {ApplicationId}", appIdProvider.GetId());
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
