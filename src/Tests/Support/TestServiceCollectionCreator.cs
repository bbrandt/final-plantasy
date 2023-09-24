using Microsoft.Extensions.DependencyInjection;
using TRS.FinalPlantasy.Application.Extensions;
using TRS.FinalPlantasy.Infrastructure.Sqlite.Extensions;
using TRS.FinalPlantasy.Infrastructure.SqlServer.Extensions;

namespace TRS.FinalPlantasy.Tests.Support;

internal static class TestServiceCollectionCreator
{
    public static IServiceCollection Create(
        IEnumerable<TestConfiguration>? newConfiguration = null) 
    {
        var services = new ServiceCollection();

        var configuration = TestConfigurationBuilder.Build(newConfiguration ?? Enumerable.Empty<TestConfiguration>());

        services.AddApplication(configuration);

        services.AddLogging();

        return services;
    }

    public static IServiceCollection CreateWithSqlServer(
        IEnumerable<TestConfiguration>? newConfiguration = null)
    {
        var services = Create(newConfiguration);

        services.AddSqlServerPersistence();

        return services;
    }

    public static IServiceCollection CreateWithSqlite(
        IEnumerable<TestConfiguration>? newConfiguration = null)
    {
        var services = Create(newConfiguration);

        services.AddSqlitePersistence();

        return services;
    }
}
