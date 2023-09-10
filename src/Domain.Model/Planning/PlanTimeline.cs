namespace TRS.FinalPlantasy.Domain.Model.Planning;

public class PlanTimeline
{
    public PlanTimeline(IEnumerable<PlanEventWithBalance> events)
    {
        Events = events.ToList();
    }

    public IReadOnlyCollection<PlanEventWithBalance> Events { get; }
}