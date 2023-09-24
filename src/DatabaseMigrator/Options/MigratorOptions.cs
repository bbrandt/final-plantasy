using Microsoft.Data.Sqlite;

namespace TRS.FinalPlantasy.DatabaseMigrator.Options;

public class MigratorOptions
{
    public MigratorDatabaseType DatabaseType { get; set; }

    public string? ConnectionString { get; set; }

    public static MigratorOptions Default()
    {
        var builder = new SqliteConnectionStringBuilder
        {
            DataSource = "./litefs/final-plantasy.db"
        };

        return new MigratorOptions
        {
            DatabaseType = MigratorDatabaseType.Sqlite,
            ConnectionString = builder.ConnectionString
        };
    }
}
