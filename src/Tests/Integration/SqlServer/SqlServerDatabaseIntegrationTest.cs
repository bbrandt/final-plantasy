﻿using System.Collections.ObjectModel;
using Testcontainers.MsSql;
using TRS.FinalPlantasy.Application.Abstractions.Options;
using TRS.FinalPlantasy.DatabaseMigrator.Application;
using TRS.FinalPlantasy.DatabaseMigrator.Options;
using TRS.FinalPlantasy.Tests.Support;

namespace TRS.FinalPlantasy.Tests.Integration.SqlServer;

public abstract class SqlServerDatabaseIntegrationTest
{
    private readonly IDictionary<string, MsSqlContainer> _testContainers;

    public SqlServerDatabaseIntegrationTest()
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

    protected abstract void TearDown();

    private static async Task StartSqlServerAsync(MsSqlContainer sqlContainer)
    {
        await sqlContainer.StartAsync();

        var runner = new DatabaseMigrationRunner();

        var options = new MigratorOptions
        {
            ConnectionString = sqlContainer.GetConnectionString(),
            DatabaseType = MigratorDatabaseType.SqlServer
        };

        await runner.RunAsync(options, default);
    }

    [TearDown]
    public async Task Dispose()
    {
        var container = _testContainers[TestContext.CurrentContext.Test.Name];

        TearDown();

        await container.DisposeAsync();
    }
}
