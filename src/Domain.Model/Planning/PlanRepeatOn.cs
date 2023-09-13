namespace TRS.FinalPlantasy.Domain.Model.Planning;

public class PlanRepeatOn
{
    public PlanRepeatOn(PlanRepeatOnId id, string name)
    {
        Id = id;
        Name = name;
    }

    public PlanRepeatOnId Id { get; protected set; }

    public string Name { get; protected set; }
}
