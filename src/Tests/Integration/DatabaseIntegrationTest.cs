using System.Collections.ObjectModel;
using Testcontainers.MsSql;
using TRS.FinalPlantasy.Application.Abstractions.Options;
using TRS.FinalPlantasy.DatabaseMigrator.Application;
using TRS.FinalPlantasy.DatabaseMigrator.Options;
using TRS.FinalPlantasy.Tests.Support;

namespace TRS.FinalPlantasy.Tests.Integration;

public abstract class DatabaseIntegrationTest
{
    private readonly IDictionary<string, MsSqlContainer> _testContainers;
    
    public DatabaseIntegrationTest()
    {
        _testContainers = new Dictionary<string, MsSqlContainer>();
    }

    [SetUp]
    public async Task InitAsync()
    {
        var container = new MsSqlBuilder().Build();

        await StartSqlServerAsync(container);

        var configuration = new Collection<TestConfiguration>()
        {
            new TestConfiguration($"{nameof(PlanningOptions)}:{nameof(PlanningOptions.DatabaseConnectionString)}", container.GetConnectionString())
        };

        Setup(configuration);

        _testContainers.Add(TestContext.CurrentContext.Test.Name, container);
    }

    protected abstract void Setup(IEnumerable<TestConfiguration> configuration);

    private static async Task StartSqlServerAsync(MsSqlContainer sqlContainer)
    {
        await sqlContainer.StartAsync();

        var runner = new DatabaseMigrationRunner();

        var options = new MigratorOptions
        {
            ConnectionString = sqlContainer.GetConnectionString()
        };

        await runner.RunAsync(options, default);
    }

    [TearDown]
    public async Task TearDown()
    {
        var container = _testContainers[TestContext.CurrentContext.Test.Name];

        await container.DisposeAsync();
    }
}
