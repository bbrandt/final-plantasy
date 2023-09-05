using Microsoft.EntityFrameworkCore;
using TRS.FinalPlantasy.Domain.Model.Planning;
using TRS.FinalPlantasy.Infrastructure.SqlServer.Common;

namespace TRS.FinalPlantasy.Infrastructure.SqlServer.Planning;

internal class PlanningContextModelBuilder
{
    public void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Planning);

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
