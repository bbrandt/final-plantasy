using TRS.FinalPlantasy.Domain.Model.Planning;

namespace TRS.FinalPlantasy.Domain.Planning.Timelines;

internal class ExpandedEvent
{
    public ExpandedEvent(
        DateOnly date,
        PlanEntry entry)
    {
        Date = date;
        Entry = entry;
    }

    public DateOnly Date { get; protected set; }

    public PlanEntry Entry { get; protected set; }
}

