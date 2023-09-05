using TRS.FinalPlantasy.Application.Extensions;
using TRS.FinalPlantasy.Infrastructure.SqlServer.Extensions;

namespace TRS.FinalPlantasy.Web.Infrastructure;

internal static class ApplicationWebApplicationBuilderCreator
{
    public static WebApplicationBuilder Create(string[] args, Serilog.ILogger logger)
    {
        logger.Information("Creating web application builder");

        var builder = WebApplication.CreateBuilder(args);

        logger.Information("Adding services");

        builder.Services.AddControllersWithViews();
        builder.Services.AddSwaggerGen();

        builder.Services.AddApplication(builder.Configuration);
        builder.Services.AddSqlServerPersistence();

        return builder;
    }
}


