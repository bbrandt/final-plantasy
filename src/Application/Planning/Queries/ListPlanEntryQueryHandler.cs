using MediatR;
using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Queries;

namespace TRS.FinalPlantasy.Application.Planning.Queries;

internal class ListPlanEntryQueryHandler : IRequestHandler<ListPlanEntryQuery, IEnumerable<PlanEntryListModel>>
{
    private readonly IPlanningQueryContext _queryContext;

    public ListPlanEntryQueryHandler(IPlanningQueryContext queryContext)
    {
        _queryContext = queryContext;
    }

    public Task<IEnumerable<PlanEntryListModel>> Handle(ListPlanEntryQuery request, CancellationToken cancellationToken)
    {
        var models = from entity in _queryContext.PlanEntries
                     join planType in _queryContext.PlanTypes on entity.PlanType equals planType.Id
                     join repeatOn in _queryContext.PlanRepeatOns on entity.RepeatOn equals repeatOn.Id
                     orderby entity.EventDate
                     select new PlanEntryListModel 
                     { 
                         Id = entity.Id,
                         Amount = entity.Amount,
                         Description = entity.Description,
                         EventDate = entity.EventDate,
                         EndDate = entity.EndDate,
                         PlanTypeName = planType.Name,
                         PlanType = entity.PlanType,
                         RepeatOnName = repeatOn.Name,
                         RepeatOn = entity.RepeatOn
                     };

        return Task.FromResult(models.AsEnumerable());
    }
}
