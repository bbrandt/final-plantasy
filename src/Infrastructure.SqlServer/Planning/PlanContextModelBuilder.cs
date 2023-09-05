using Microsoft.EntityFrameworkCore;
using TRS.FinalPlantasy.Domain.Model.Planning;

namespace TRS.FinalPlantasy.Infrastructure.SqlServer.Planning;

internal class PlanContextModelBuilder
{
    public void Build(ModelBuilder modelBuilder)
    {
        MapPlan(modelBuilder);
    }

    private static void MapPlan(ModelBuilder modelBuilder)
    {
        var mapping = modelBuilder.Entity<PlanEntry>();

        mapping.Property(x => x.Id)
            .UseIdentityColumn()
            .ValueGeneratedNever();

        mapping.HasKey(x => x.Id);
    }
}
