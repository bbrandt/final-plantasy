﻿using Microsoft.EntityFrameworkCore;

namespace TRS.FinalPlantasy.Infrastructure.EntityFramework.Planning;

internal class PlanningContext : DbContext
{
    private readonly IPlanningContextConfiguration _configuration;
    private readonly PlanningContextModelBuilder _modelBuilder;

    public PlanningContext(
        IPlanningContextConfiguration configuration,
        PlanningContextModelBuilder modelBuilder)
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

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>()
            .HaveColumnType("date");
    }
}
