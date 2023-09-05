using Microsoft.Extensions.DependencyInjection;
using TRS.FinalPlantasy.Application.Extensions;
using TRS.FinalPlantasy.Infrastructure.SqlServer.Extensions;

namespace TRS.FinalPlantasy.Tests.Support;

internal static class TestServiceCollectionCreator
{
    public static IServiceCollection Create(IEnumerable<TestConfiguration>? newConfiguration = null) 
    {
        var services = new ServiceCollection();

        var configuration = TestConfigurationBuilder.Build(newConfiguration ?? Enumerable.Empty<TestConfiguration>());

        services.AddApplication(configuration);
        services.AddSqlServerPersistence();

        return services;
    }
}
