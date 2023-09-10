using TRS.FinalPlantasy.Domain.Model.Planning;

namespace TRS.FinalPlantasy.Domain.Abstractions.Planning;

public interface IPlanTimelineDomainService
{
    PlanTimeline Calculate(
        DateOnly endDate,
        IEnumerable<PlanEntry> entries);
}
