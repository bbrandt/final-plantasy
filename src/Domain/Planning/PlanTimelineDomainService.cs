using System.Collections.ObjectModel;
using TRS.FinalPlantasy.Domain.Model.Planning;

namespace TRS.FinalPlantasy.Domain.Planning;

public class PlanTimelineDomainService
{
    public PlanTimeline Calculate(IEnumerable<PlanEntry> entries)
    {
        var events = new Collection<PlanEvent>();

        var timeline = new PlanTimeline(events);

        return timeline;
    }
}
