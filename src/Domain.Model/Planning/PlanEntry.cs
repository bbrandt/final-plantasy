namespace TRS.FinalPlantasy.Domain.Model.Planning;

public class PlanEntry
{
    // The describe function will set the description value or throw
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected PlanEntry(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        PlanType planType,
        DateOnly eventDate,
        double amount,
        PlanRepeatOn repeatOn,
        string description)
    {
        Update(
            planType,
            eventDate,
            amount,
            repeatOn,
            description);
    }

    public static PlanEntry NewEntry(
        PlanType planType,
        DateOnly eventDate,
        double amount,
        PlanRepeatOn repeatOn,
        string description)
    {     
        return new PlanEntry(
            planType, 
            eventDate, 
            amount, 
            repeatOn,
            description);
    }

    public void Update(
        PlanType planType,
        DateOnly eventDate,
        double amount,
        PlanRepeatOn repeatOn,
        string description)
    {
        CreditOrDebit(planType)
            .OnEventDate(eventDate)
            .ForAmount(amount)
            .RepeatPlanOn(repeatOn)
            .DescribedAs(description);
    }

    public int Id { get; protected set; }

    public PlanType PlanType { get; protected set; }

    public double Amount { get; protected set; }

    public DateOnly EventDate { get; protected set; }

    public PlanRepeatOn RepeatOn { get; protected set; }

    public string Description { get; protected set; }

    public PlanEntry CreditOrDebit(PlanType planType)
    {
        PlanType = planType;

        return this;
    }

    public PlanEntry OnEventDate(DateOnly eventDate)
    {
        if (eventDate == DateOnly.MinValue || eventDate == DateOnly.MaxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(eventDate));
        }

        EventDate = eventDate;

        return this;
    }

    public PlanEntry DescribedAs(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentOutOfRangeException(nameof(description));
        }

        Description = description;

        return this;
    }

    public PlanEntry ForAmount(double amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        Amount = amount;

        return this;
    }

    public PlanEntry RepeatPlanOn(PlanRepeatOn repeatOn)
    {
        RepeatOn = repeatOn;

        return this;
    }
}
