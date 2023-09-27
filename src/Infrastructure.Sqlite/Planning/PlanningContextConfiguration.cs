using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TRS.FinalPlantasy.Application.Abstractions.Options;
using TRS.FinalPlantasy.Infrastructure.EntityFramework.Planning;

namespace TRS.FinalPlantasy.Infrastructure.Sqlite.Planning;

internal class PlanningContextConfiguration : IPlanningContextConfiguration
{
    private readonly IOptionsSnapshot<PlanningOptions> _options;
    private readonly ILogger<PlanningContextConfiguration> _logger;

    public PlanningContextConfiguration(IOptionsSnapshot<PlanningOptions> options, ILogger<PlanningContextConfiguration> logger)
    {
        _options = options;
        _logger = logger;
    }

    public void Configure(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _options.Value.DatabaseConnectionString;

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new Exception($"Connection string is not defined for {nameof(PlanningOptions)}");
        }
        else
        {
            _logger.LogInformation("Connecting to {ConnectionString}", connectionString);
        }

        optionsBuilder.UseSqlite(connectionString);
    }
}


