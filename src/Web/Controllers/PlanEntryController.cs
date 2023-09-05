using MediatR;
using Microsoft.AspNetCore.Mvc;
using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Commands;
using TRS.FinalPlantasy.Application.Abstractions.Validations;

namespace TRS.FinalPlantasy.Web.Controllers;

[ApiController]
[Route("/api/plan-entry")]
public class PlanEntryController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlanEntryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("/api/plan-entry/list")]
    public IEnumerable<PlanEntryModel> Get()
    {
        
    }

    [HttpPost]
    [Route("/api/plan-entry/add-or-update")]
    public async Task<ResultResponse<int?>> AddOrUpdate(PlanEntryModel model)
    {
        var command = new NewPlanEntryCommand 
        { 
            Model = model
        };

        var response = await _mediator.Send(command);

        return response;
    }
}
