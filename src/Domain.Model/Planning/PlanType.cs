namespace TRS.FinalPlantasy.Domain.Model.Planning;

public class PlanType
{
    public PlanType(PlanTypeId id, string name)
    {
        Id = id;
        Name = name;
    }

    public PlanTypeId Id { get; protected set; }

    public string Name { get; protected set; }
}
