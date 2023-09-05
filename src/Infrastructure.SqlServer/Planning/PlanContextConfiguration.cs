using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TRS.FinalPlantasy.Application.Abstractions.Options;

namespace TRS.FinalPlantasy.Infrastructure.SqlServer.Planning;

internal class PlanContextConfiguration
{
    private readonly IOptionsSnapshot<PlanningOptions> _options;

    public PlanContextConfiguration(IOptionsSnapshot<PlanningOptions> options)
    {
        _options = options;
    }

    public void Configure(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _options.Value.DatabaseConnectionString;

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new Exception("Connection string is not defined for Plan options");
        }

        optionsBuilder.UseSqlServer(connectionString);
    }
}
