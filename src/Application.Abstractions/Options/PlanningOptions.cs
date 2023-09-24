namespace TRS.FinalPlantasy.Application.Abstractions.Options;

public class PlanningOptions
{
    public string? DatabaseConnectionString { get; set; }

    public ApplicationDatabaseType DatabaseType { get; set; }
}
