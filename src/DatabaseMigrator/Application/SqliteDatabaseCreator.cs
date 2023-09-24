namespace TRS.FinalPlantasy.DatabaseMigrator.Application;

internal class SqliteDatabaseCreator : IDatabaseCreator
{
    public Task CreateAsync(string connectionString, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
