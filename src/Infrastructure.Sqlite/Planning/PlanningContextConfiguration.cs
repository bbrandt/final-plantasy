using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TRS.FinalPlantasy.Application.Abstractions.Options;
using TRS.FinalPlantasy.Infrastructure.EntityFramework.Planning;

namespace TRS.FinalPlantasy.Infrastructure.Sqlite.Planning;

internal class PlanningContextConfiguration : IPlanningContextConfiguration
{
    private readonly IOptionsSnapshot<PlanningOptions> _options;

    public PlanningContextConfiguration(IOptionsSnapshot<PlanningOptions> options)
    {
        _options = options;
    }

    public void Configure(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _options.Value.DatabaseConnectionString;

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new Exception($"Connection string is not defined for {nameof(PlanningOptions)}");
        }

        optionsBuilder.UseSqlite(connectionString);
    }
}


