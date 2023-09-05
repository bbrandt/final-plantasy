using Microsoft.EntityFrameworkCore;

namespace TRS.FinalPlantasy.Infrastructure.SqlServer.Planning;

internal class PlanContext : DbContext
{
    private readonly PlanContextConfiguration _configuration;
    private readonly PlanContextModelBuilder _modelBuilder;

    public PlanContext(
        PlanContextConfiguration configuration,
        PlanContextModelBuilder modelBuilder)
    {
        _configuration = configuration;
        _modelBuilder = modelBuilder;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _configuration.Configure(optionsBuilder);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _modelBuilder.Build(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
}
