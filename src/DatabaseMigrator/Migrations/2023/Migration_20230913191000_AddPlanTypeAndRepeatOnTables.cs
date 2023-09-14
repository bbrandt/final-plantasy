using FluentMigrator;
using TRS.FinalPlantasy.DatabaseMigrator.Infrastructure;

namespace TRS.FinalPlantasy.DatabaseMigrator.Migrations._2023;

[Migration(20230913191000)]
public class Migration_20230913191000_AddPlanTypeAndRepeatOnTables : ForwardMigration
{
    public override void Up()
    {
        CreatePlanTypeTable();
        CreatePlanRepeatOnTable();
    }

    private void CreatePlanTypeTable()
    {
        var table = "PlanType";

        Create.Table(table).InSchema(Schemas.Planning)
            .WithColumn("Id").AsInt32().PrimaryKey()
            .WithColumn("Name").AsString(255).NotNullable();

        Insert.IntoTable(table).InSchema(Schemas.Planning)
            .Row(new 
                { 
                    Id = 0,
                    Name = "Credit"
                });

        Insert.IntoTable(table).InSchema(Schemas.Planning)
            .Row(new
            {
                Id = 1,
                Name = "Debit"
            });

        Create.ForeignKey()
            .FromTable("PlanEntry").InSchema(Schemas.Planning)
            .ForeignColumn("PlanType").ToTable(table).InSchema(Schemas.Planning)
            .PrimaryColumn("Id");
    }

    private void CreatePlanRepeatOnTable()
    {
        var table = "PlanRepeatOn";

        Create.Table(table).InSchema(Schemas.Planning)
            .WithColumn("Id").AsInt32().PrimaryKey()
            .WithColumn("Name").AsString(255).NotNullable();

        Insert.IntoTable(table).InSchema(Schemas.Planning)
            .Row(new
            {
                Id = 0,
                Name = "None"
            });

        Insert.IntoTable(table).InSchema(Schemas.Planning)
            .Row(new
            {
                Id = 1,
                Name = "Bi-Weekly"
            });

        Insert.IntoTable(table).InSchema(Schemas.Planning)
            .Row(new
            {
                Id = 2,
                Name = "Monthly"
            });

        Insert.IntoTable(table).InSchema(Schemas.Planning)
            .Row(new
            {
                Id = 3,
                Name = "Yearly"
            });

        Create.ForeignKey()
            .FromTable("PlanEntry").InSchema(Schemas.Planning)
            .ForeignColumn("RepeatOn").ToTable(table).InSchema(Schemas.Planning)
            .PrimaryColumn("Id");
    }
}
