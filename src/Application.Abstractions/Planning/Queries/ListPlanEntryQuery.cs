using MediatR;

namespace TRS.FinalPlantasy.Application.Abstractions.Planning.Queries;

internal class ListPlanEntryQuery : IRequest<IEnumerable<PlanEntryModel>>
{
}
