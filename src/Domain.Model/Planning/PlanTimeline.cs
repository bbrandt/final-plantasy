namespace TRS.FinalPlantasy.Domain.Model.Planning;

public class PlanTimeline
{
    public PlanTimeline(IEnumerable<PlanEvent> events)
    {
        Events = events.ToList();
    }

    public IReadOnlyCollection<PlanEvent> Events { get; }
}