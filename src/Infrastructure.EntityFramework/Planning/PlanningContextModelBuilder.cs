using Microsoft.EntityFrameworkCore;
using TRS.FinalPlantasy.Domain.Model.Planning;
using TRS.FinalPlantasy.Infrastructure.EntityFramework.Common;

namespace TRS.FinalPlantasy.Infrastructure.EntityFramework.Planning;

internal class PlanningContextModelBuilder
{
    public void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Planning);

        MapPlan(modelBuilder);
        MapPlanType(modelBuilder);
        MapRepeatOn(modelBuilder);
    }

    private static void MapPlan(ModelBuilder modelBuilder)
    {
        var mapping = modelBuilder.Entity<PlanEntry>();

        mapping.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        mapping.HasKey(x => x.Id);
    }

    private static void MapPlanType(ModelBuilder modelBuilder)
    {
        var mapping = modelBuilder.Entity<PlanType>();

        mapping.HasKey(x => x.Id);
    }

    private static void MapRepeatOn(ModelBuilder modelBuilder)
    {
        var mapping = modelBuilder.Entity<PlanRepeatOn>();

        mapping.HasKey(x => x.Id);
    }
}
