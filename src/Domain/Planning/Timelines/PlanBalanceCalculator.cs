using TRS.FinalPlantasy.Domain.Model.Planning;

namespace TRS.FinalPlantasy.Domain.Planning.Timelines;

internal class PlanBalanceCalculator
{
    public IEnumerable<PlanEventWithBalance> Calculate(
        IEnumerable<ExpandedEvent> expandedEvents)
    {
        var balance = 0d;

        var timeline = expandedEvents
            .GroupBy(x => x.Date)
            .Select((eventGroup) =>
            {
                var groupedEvents = eventGroup.ToList();
                var eventDate = eventGroup.Key;

                var planEvent = CreatePlanEvent(balance, eventDate, groupedEvents);

                balance = planEvent.Balance;

                return planEvent;
            });

        return timeline;
    }

    private static PlanEventWithBalance CreatePlanEvent(
        double priorBalance,
        DateOnly eventDate,
        IReadOnlyCollection<ExpandedEvent> expandedEvents)
    {
        var newBalance = CalculateBalance(priorBalance, expandedEvents);

        var descriptions = expandedEvents
            .Select(x => x.Entry.Description)
            .OrderBy(x => x);

        return new PlanEventWithBalance(
            eventDate,
            newBalance,
            descriptions);
    }

    private static double CalculateBalance(double balance, IEnumerable<ExpandedEvent> expandedEvents)
    {
        var newBalance = expandedEvents
            .Aggregate(balance, CalculateBalance);

        return newBalance;
    }

    private static double CalculateBalance(double balance, ExpandedEvent expandedEvent)
    {
        switch (expandedEvent.Entry.PlanType)
        {
            case PlanType.Credit:
                return balance + expandedEvent.Entry.Amount;
            case PlanType.Debit:
                return balance - expandedEvent.Entry.Amount;
            default:
                throw new Exception("Unsupported plan type");
        }
    }
}

