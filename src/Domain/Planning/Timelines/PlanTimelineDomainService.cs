using TRS.FinalPlantasy.Domain.Abstractions.Planning;
using TRS.FinalPlantasy.Domain.Model.Planning;

namespace TRS.FinalPlantasy.Domain.Planning.Timelines;

internal class PlanTimelineDomainService : IPlanTimelineDomainService
{
    private readonly ExpandedEventCalculator _expandedEventCalculator;
    private readonly PlanBalanceCalculator _planBalanceCalculator;

    public PlanTimelineDomainService(
        ExpandedEventCalculator expandedEventCalculator,
        PlanBalanceCalculator planBalanceCalculator)
    {
        _expandedEventCalculator = expandedEventCalculator;
        _planBalanceCalculator = planBalanceCalculator;
    }

    public PlanTimeline Calculate(
        DateOnly endDate,
        IEnumerable<PlanEntry> entries)
    {
        var expandedEvents = _expandedEventCalculator.Calculate(
            endDate,
            entries);

        var eventsWithBalance = _planBalanceCalculator.Calculate(expandedEvents);

        var timeline = new PlanTimeline(eventsWithBalance);

        return timeline;
    }    
}

