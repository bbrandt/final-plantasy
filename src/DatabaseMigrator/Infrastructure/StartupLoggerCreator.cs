using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.SystemConsole.Themes;

namespace TRS.FinalPlantasy.DatabaseMigrator.Infrastructure;

internal static class StartupLoggerCreator
{
    public static ILogger Create()
    {
        var loggerConfiguration = new LoggerConfiguration()
            .Enrich.WithMachineName()
            .Enrich.WithProcessId()
            .Enrich.WithExceptionDetails()
            .Enrich.FromLogContext()
            .WriteTo.Console(theme: AnsiConsoleTheme.Code);

        return loggerConfiguration.CreateLogger();
    }
}
