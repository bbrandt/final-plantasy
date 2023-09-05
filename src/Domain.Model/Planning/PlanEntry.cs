namespace TRS.FinalPlantasy.Domain.Model.Planning;

public class PlanEntry
{
    public PlanEntry(
        PlanType planType,
        DateOnly eventDate,
        double amount,
        PlanRepeatOn? repeatOn)
    {
        PlanType = planType;
        EventDate = eventDate;
        Amount = amount;
        RepeatOn = repeatOn;
    }

    public int Id { get; protected set; }

    public PlanType PlanType { get; protected set; }

    public double Amount { get; protected set; }

    public DateOnly EventDate { get; protected set; }

    public PlanRepeatOn? RepeatOn { get; protected set; }
}
