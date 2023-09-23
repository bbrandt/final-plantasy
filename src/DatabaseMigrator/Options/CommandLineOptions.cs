using CommandLine;

namespace TRS.FinalPlantasy.DatabaseMigrator.Options;

internal class CommandLineOptions
{
    [Option('c', "connection-string", Required = false, HelpText = "The connection string of the database.")]
    public string? ConnectionString { get; set; }

    [Option('t', "type", Required = false, HelpText = "The type of the database Sqlite or SqlServer.")]
    public MigratorDatabaseType DatabaseType { get; set; }
}
