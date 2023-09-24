namespace TRS.FinalPlantasy.DatabaseMigrator.Application;

internal interface IDatabaseCreator
{
    Task CreateAsync(
        string connectionString,
        CancellationToken cancellationToken);
}
