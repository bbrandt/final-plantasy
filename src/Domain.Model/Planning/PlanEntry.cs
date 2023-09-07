namespace TRS.FinalPlantasy.Domain.Model.Planning;

public class PlanEntry
{
    protected PlanEntry(
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

    public static PlanEntry NewEntry(
        PlanType planType,
        DateOnly eventDate,
        double amount,
        PlanRepeatOn? repeatOn)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        if (eventDate == DateOnly.MinValue || eventDate == DateOnly.MaxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(eventDate));
        }

        return new PlanEntry(
            planType, 
            eventDate, 
            amount, repeatOn);
    }

    public int Id { get; protected set; }

    public PlanType PlanType { get; protected set; }

    public double Amount { get; protected set; }

    public DateOnly EventDate { get; protected set; }

    public PlanRepeatOn? RepeatOn { get; protected set; }
}
