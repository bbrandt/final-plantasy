using MediatR;
using TRS.FinalPlantasy.Application.Abstractions.Validations;

namespace TRS.FinalPlantasy.Application.Abstractions.Planning.Commands;

public class AddPlanEntryCommand : IRequest<ResultResponse<int?>>
{
    public PlanEntryModel? Model { get; set; }
}
