using MediatR;

namespace TRS.FinalPlantasy.Application.Abstractions.Planning.Queries;

public class PlanEntryByIdQuery : IRequest<PlanEntryModel?>
{
    public int Id { get; set; }
}
