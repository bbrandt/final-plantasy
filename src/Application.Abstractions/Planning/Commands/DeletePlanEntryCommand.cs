using MediatR;
using TRS.FinalPlantasy.Application.Abstractions.Validations;

namespace TRS.FinalPlantasy.Application.Abstractions.Planning.Commands;

public class DeletePlanEntryCommand : IRequest<Response>
{
    public int Id { get; set; }
}
