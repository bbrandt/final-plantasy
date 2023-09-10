using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Queries;
using TRS.FinalPlantasy.Domain.Abstractions.Planning;

namespace TRS.FinalPlantasy.Application.Planning;

internal class PlanTimelineApplicationService : IPlanTimelineApplicationService
{
    private readonly IPlanTimelineDomainService _domainService;
    private readonly IPlanningQueryContext _queryContext;

    public PlanTimelineApplicationService(
        IPlanTimelineDomainService domainService,
        IPlanningQueryContext queryContext)
    {
        _domainService = domainService;
        _queryContext = queryContext;
    }

    public PlanTimelineModel CalculatePlanTimeline(DateOnly endDate)
    {
        var entries = _queryContext.PlanEntries
            .OrderBy(x => x.EventDate)
            .ToList();

        var timeline = _domainService.Calculate(endDate, entries);

        var eventModels = timeline.Events
            .Select(x => new PlanEventWithBalanceModel(x.Date, x.Balance, x.Descriptions));

        var model = new PlanTimelineModel(eventModels);

        return model;
    }
}
