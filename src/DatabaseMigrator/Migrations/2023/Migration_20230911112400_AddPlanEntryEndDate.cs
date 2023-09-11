using FluentMigrator;
using TRS.FinalPlantasy.DatabaseMigrator.Infrastructure;

namespace TRS.FinalPlantasy.DatabaseMigrator.Migrations._2023;

[Migration(20230911112400)]
public class Migration_20230911112400_AddPlanEntryEndDate : ForwardMigration
{
    public override void Up()
    {
        const string Table = "PlanEntry";

        Alter.Table(Table).InSchema(Schemas.Planning)
            .AddColumn("EndDate").AsDateTime2().Nullable();
    }
}
