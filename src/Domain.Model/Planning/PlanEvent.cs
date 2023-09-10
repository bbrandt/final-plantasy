namespace TRS.FinalPlantasy.Domain.Model.Planning;

public class PlanEvent
{
    public DateOnly Date { get; protected set; }

    public double Value { get; protected set; }

    public string? Description { get; protected set; }
}
