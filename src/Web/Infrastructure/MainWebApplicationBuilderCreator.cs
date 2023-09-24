using TRS.FinalPlantasy.Application.Abstractions.Options;
using TRS.FinalPlantasy.Application.Extensions;
using TRS.FinalPlantasy.Infrastructure.SqlServer.Extensions;
using TRS.FinalPlantasy.Infrastructure.Sqlite.Extensions;

namespace TRS.FinalPlantasy.Web.Infrastructure;

internal static class MainWebApplicationBuilderCreator
{
    public static WebApplicationBuilder Create(string[] args, Serilog.ILogger logger)
    {
        logger.Information("Creating web application builder");

        var builder = WebApplication.CreateBuilder(args);

        logger.Information("Adding services");

        builder.Services.AddControllersWithViews();
        builder.Services.AddSwaggerGen();

        builder.Services.AddApplication(builder.Configuration);

        var options = ReadOptions(builder.Configuration);

        switch (options.DatabaseType)
        {
            case ApplicationDatabaseType.Sqlite:
                builder.Services.AddSqlitePersistence();
                break;
            case ApplicationDatabaseType.SqlServer:
                builder.Services.AddSqlServerPersistence();
                break;
        }

        return builder;
    }

    private static PlanningOptions ReadOptions(IConfiguration configuration)
    {
        var options = new PlanningOptions();

        configuration.GetRequiredSection(nameof(PlanningOptions))
            .Bind(options);

        return options;
    }
}


