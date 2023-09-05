using FluentMigrator;
using TRS.FinalPlantasy.DatabaseMigrator.Infrastructure;

namespace TRS.FinalPlantasy.DatabaseMigrator.Migrations._2023;

[Migration(20230905200000)]
public class Migration_20230905200000_AddPlanEntryTable : ForwardMigration
{
    public override void Up()
    {
        Create.Schema(Schemas.Planning);

        const string Table = "PlanEntry";

        Create.Table(Table).InSchema(Schemas.Planning)
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("PlanType").AsInt32().NotNullable()
            .WithColumn("EventDate").AsDateTime2().NotNullable()
            .WithColumn("Amount").AsDouble().NotNullable()
            .WithColumn("RepeatOn").AsInt32().Nullable();
    }
}
