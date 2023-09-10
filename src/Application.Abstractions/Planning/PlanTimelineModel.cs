namespace TRS.FinalPlantasy.Application.Abstractions.Planning;

public class PlanTimelineModel
{
    public PlanTimelineModel(IEnumerable<PlanEventWithBalanceModel> events)
    {
        Events = events.ToList();
    }

    public IReadOnlyCollection<PlanEventWithBalanceModel> Events { get; }
}
