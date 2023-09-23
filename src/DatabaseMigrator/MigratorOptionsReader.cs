using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TRS.FinalPlantasy.DatabaseMigrator.Options;

internal static class MigratorOptionsReader
{
    public static MigratorOptions Read(CommandLineOptions commandLineOptions)
    {
        var configuration = BuildConfiguration();

        var migratorOptions = new MigratorOptions
        {
            ConnectionString = commandLineOptions.ConnectionString,
            DatabaseType = commandLineOptions.DatabaseType
        };

        if (string.IsNullOrWhiteSpace(migratorOptions.ConnectionString))
        {
            configuration.GetRequiredSection(nameof(MigratorOptions))
                .Bind(migratorOptions);
        }

        return migratorOptions;
    }

    private static IConfiguration BuildConfiguration()
    {
        var builder = new ConfigurationBuilder();

        var configuration = builder.AddJsonFile("appsettings.json")
            .AddUserSecrets<Program>(optional: true)
            .Build();

        return configuration;
    }
}