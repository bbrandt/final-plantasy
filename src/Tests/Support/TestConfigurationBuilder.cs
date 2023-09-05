using Microsoft.Extensions.Configuration;

namespace TRS.FinalPlantasy.Tests.Support;

internal class TestConfigurationBuilder
{ 
    public static IConfiguration Build(IEnumerable<TestConfiguration> newConfiguration)
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true)
            .AddUserSecrets(typeof(TestConfigurationBuilder).Assembly, optional: true)
            .AddInMemoryCollection(newConfiguration.ToDictionary(x => x.Key, x => x.Value));

        var root = builder.Build();

        return root;
    }
}