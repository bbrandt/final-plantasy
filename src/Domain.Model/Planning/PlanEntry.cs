namespace TRS.FinalPlantasy.Domain.Model.Planning;

public class PlanEntry
{
    public PlanEntry(
        int id,
        PlanType planType,
        double amount,
        DateOnly eventDate)
    {
        Id = id;
        PlanType = planType;
        Amount = amount;
        EventDate = eventDate;
    }

    public int Id { get; protected set; }

    public PlanType PlanType { get; protected set; }

    public double Amount { get; protected set; }

    public DateOnly EventDate { get; protected set; }
}
