namespace TRS.FinalPlantasy.Domain.Model.Planning;

public class PlanEntry
{
    // The describe function will set the description value or throw
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected PlanEntry(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        PlanTypeId planType,
        DateOnly eventDate,
        double amount,
        PlanRepeatOnId repeatOn,
        string description,
        DateOnly? endDate)
    {
        Update(
            planType,
            eventDate,
            amount,
            repeatOn,
            description,
            endDate);
    }

    public static PlanEntry NewEntry(
        PlanTypeId planType,
        DateOnly eventDate,
        double amount,
        PlanRepeatOnId repeatOn,
        string description,
        DateOnly? endDate)
    {     
        return new PlanEntry(
            planType, 
            eventDate, 
            amount, 
            repeatOn,
            description,
            endDate);
    }

    public void Update(
        PlanTypeId planType,
        DateOnly eventDate,
        double amount,
        PlanRepeatOnId repeatOn,
        string description,
        DateOnly? endDate)
    {
        CreditOrDebit(planType)
            .OnEventDate(eventDate)
            .ForAmount(amount)
            .RepeatPlanOn(repeatOn)
            .DescribedAs(description)
            .EndsOn(endDate);
    }

    public int Id { get; protected set; }

    public PlanTypeId PlanType { get; protected set; }

    public double Amount { get; protected set; }

    public DateOnly EventDate { get; protected set; }

    public PlanRepeatOnId RepeatOn { get; protected set; }

    public string Description { get; protected set; }

    public DateOnly? EndDate { get; protected set; }

    public PlanEntry CreditOrDebit(PlanTypeId planType)
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

    public PlanEntry RepeatPlanOn(PlanRepeatOnId repeatOn)
    {
        RepeatOn = repeatOn;

        return this;
    }

    public PlanEntry EndsOn(DateOnly? endDate)
    {
        if (endDate <= EventDate)
        {
            throw new ArgumentOutOfRangeException(nameof(endDate));
        }

        EndDate = endDate;

        return this;
    }
}
