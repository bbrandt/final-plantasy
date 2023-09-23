using Microsoft.Data.SqlClient;

namespace TRS.FinalPlantasy.DatabaseMigrator.Application;

internal class SqlServerDatabaseCreator : IDatabaseCreator
{
    public async Task CreateAsync(
        string connectionString, 
        CancellationToken cancellationToken)
    {
        var databaseInformation = GetDatabaseInformation(connectionString);

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new Exception("The connection string is malformed or missing.");
        }

        if (string.IsNullOrEmpty(databaseInformation.DatabaseName))
        {
            throw new Exception("The database name was not found");
        }

        await CreateDatabaseAsync(
            databaseInformation.ConnectionString, 
            databaseInformation.DatabaseName, 
            cancellationToken);
    }

    private static async Task CreateDatabaseAsync(
        string connectionString, 
        string databaseName, 
        CancellationToken cancellationToken)
    {
        if (await IsExistingDatabaseAsync(connectionString, databaseName, cancellationToken))
        {
            return;
        }

        await ExecuteCreateDatabaseAsync(connectionString, databaseName, cancellationToken);
    }

    private static async Task ExecuteCreateDatabaseAsync(
        string connectionString,
        string databaseName,
        CancellationToken cancellationToken) 
    {
        using var connection = new SqlConnection(connectionString);

        await connection.OpenAsync(cancellationToken);

        using var command = connection.CreateCommand();

        command.CommandText = $"CREATE DATABASE {databaseName}";
        command.CommandTimeout = 0;

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task<bool> IsExistingDatabaseAsync(
        string connectionString, 
        string databaseName, 
        CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(connectionString);

        await connection.OpenAsync(cancellationToken);

        using var command = connection.CreateCommand();

        command.CommandText = "SELECT * FROM sys.databases WHERE Name = @name";

        var parameter = new SqlParameter("@name", System.Data.SqlDbType.Char)
        {
            Value = databaseName
        };

        command.Parameters.Add(parameter);

        var result = await command.ExecuteScalarAsync(cancellationToken);

        if (result != null)
        {
            return true;
        }

        return false;
    }

    private static DatabaseConnectionString GetDatabaseInformation(string connectionString)
    {
        var builder = new SqlConnectionStringBuilder(connectionString);

        var databaseName = builder.InitialCatalog;

        builder.Remove("Initial Catalog");

        return new DatabaseConnectionString(builder.ConnectionString, databaseName);
    }

    private record DatabaseConnectionString(string ConnectionString, string? DatabaseName);
}
