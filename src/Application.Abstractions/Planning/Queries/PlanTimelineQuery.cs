using MediatR;

namespace TRS.FinalPlantasy.Application.Abstractions.Planning.Queries;

public class PlanTimelineQuery : IRequest<PlanTimelineModel>
{
    public DateOnly EndDate { get; set; }
}
