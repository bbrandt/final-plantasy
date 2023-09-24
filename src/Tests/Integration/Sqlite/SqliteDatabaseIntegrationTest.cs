using Docker.DotNet.Models;
using Microsoft.Data.Sqlite;
using System.Collections.ObjectModel;
using TRS.FinalPlantasy.Application.Abstractions.Options;
using TRS.FinalPlantasy.DatabaseMigrator.Application;
using TRS.FinalPlantasy.DatabaseMigrator.Options;
using TRS.FinalPlantasy.Tests.Support;

namespace TRS.FinalPlantasy.Tests.Integration.Sqlite;

public abstract class SqliteDatabaseIntegrationTest
{
    private readonly IDictionary<string, SqliteInfo> _testDatabases;

    public SqliteDatabaseIntegrationTest()
    {
        _testDatabases = new Dictionary<string, SqliteInfo>();
    }

    [SetUp]
    public async Task InitAsync()
    {
        var sqliteInfo = CreateSqliteFileInfo();

        _testDatabases.Add(TestContext.CurrentContext.Test.Name, sqliteInfo);

        await MigrateSqliteAsync(sqliteInfo.ConnectionString);

        var configuration = new Collection<TestConfiguration>()
        {
            new TestConfiguration($"{nameof(PlanningOptions)}:{nameof(PlanningOptions.DatabaseConnectionString)}", sqliteInfo.ConnectionString)
        };

        Setup(configuration);
    }

    protected abstract void Setup(IEnumerable<TestConfiguration> configuration);

    protected abstract void TearDown();

    private static async Task MigrateSqliteAsync(string connectionString)
    {
        var runner = new DatabaseMigrationRunner();

        var options = new MigratorOptions
        {
            ConnectionString = connectionString,
            DatabaseType = MigratorDatabaseType.Sqlite
        };

        await runner.RunAsync(options, default);
    }

    private static SqliteInfo CreateSqliteFileInfo()
    {
        var sqliteFileInfo = CreateSqliteFile();

        var connectionString = CreateConnectionString(sqliteFileInfo);

        return new SqliteInfo(sqliteFileInfo, connectionString);
    }

    private static string CreateConnectionString(FileInfo file)
    {
        var builder = new SqliteConnectionStringBuilder
        {
            DataSource = file.FullName,
            Pooling = false
        };

        return builder.ConnectionString;
    }

    private static FileInfo CreateSqliteFile()
    {
        var fileName = GetSqliteFileName();

        using var fs = File.Create(fileName);

        var fileInfo = new FileInfo(fileName);

        return fileInfo;
    }

    private static string GetSqliteFileName()
    {
        var fileId = Guid.NewGuid();
        var fileName = $"./{fileId}.db";

        return fileName;
    }

    [TearDown]
    public Task Dispose()
    {
        var sqliteInfo = _testDatabases[TestContext.CurrentContext.Test.Name];

        TearDown();

        GC.Collect();

        TestFileDeleter.Delete(sqliteInfo.File.FullName);

        return Task.CompletedTask;
    }

    private record SqliteInfo(FileInfo File, string ConnectionString);
}
