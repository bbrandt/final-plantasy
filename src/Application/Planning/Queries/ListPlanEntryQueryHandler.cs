using MediatR;
using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Queries;

namespace TRS.FinalPlantasy.Application.Planning.Queries;

internal class ListPlanEntryQueryHandler : IRequestHandler<ListPlanEntryQuery, IEnumerable<PlanEntryModel>>
{
    private readonly IPlanningQueryContext _queryContext;

    public ListPlanEntryQueryHandler(IPlanningQueryContext queryContext)
    {
        _queryContext = queryContext;
    }

    public Task<IEnumerable<PlanEntryModel>> Handle(ListPlanEntryQuery request, CancellationToken cancellationToken)
    {
        var entries = _queryContext.PlanEntries
            .OrderBy(x => x.EventDate)
            .Select(entry =>
                new PlanEntryModel
                {
                    Id = entry.Id,
                    PlanType = entry.PlanType,
                    EventDate = entry.EventDate,
                    Amount = entry.Amount,
                    RepeatOn = entry.RepeatOn
                })
            .AsEnumerable();

        return Task.FromResult(entries);
    }
}
