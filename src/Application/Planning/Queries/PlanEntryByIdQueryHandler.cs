using MediatR;
using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Queries;

namespace TRS.FinalPlantasy.Application.Planning.Queries;

internal class PlanEntryByIdQueryHandler : IRequestHandler<PlanEntryByIdQuery, PlanEntryModel?>
{
    private readonly IPlanningQueryContext _queryContext;

    public PlanEntryByIdQueryHandler(IPlanningQueryContext queryContext)
    {
        _queryContext = queryContext;
    }

    public Task<PlanEntryModel?> Handle(PlanEntryByIdQuery request, CancellationToken cancellationToken)
    {
        var entry = _queryContext.PlanEntries
            .Where(x => x.Id == request.Id)
            .Select(entry =>
                new PlanEntryModel
                {
                    Id = entry.Id,
                    PlanType = entry.PlanType,
                    EventDate = entry.EventDate,
                    Amount = entry.Amount,
                    RepeatOn = entry.RepeatOn,
                    Description = entry.Description
                })
            .SingleOrDefault();

        return Task.FromResult(entry);
    }
}
