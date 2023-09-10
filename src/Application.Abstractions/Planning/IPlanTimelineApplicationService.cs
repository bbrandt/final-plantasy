namespace TRS.FinalPlantasy.Application.Abstractions.Planning;

public interface IPlanTimelineApplicationService
{
    PlanTimelineModel CalculatePlanTimeline(DateOnly endDate);
}
