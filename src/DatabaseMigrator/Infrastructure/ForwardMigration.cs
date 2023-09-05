using FluentMigrator;

namespace TRS.FinalPlantasy.DatabaseMigrator.Infrastructure;

public abstract class ForwardMigration : Migration
{
    public override void Down()
    {
        throw new NotImplementedException("Downward migrations are not supported.");
    }
}
