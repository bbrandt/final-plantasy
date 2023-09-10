using MediatR;
using Microsoft.AspNetCore.Mvc;
using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Commands;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Queries;
using TRS.FinalPlantasy.Application.Abstractions.Repositories;
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
    public async Task<IEnumerable<PlanEntryModel>> Get()
    {
        var query = new ListPlanEntryQuery();

        var models = await _mediator.Send(query);

        return models;
    }

    [HttpGet]
    [Route("/api/plan-entry/{id}")]
    public async Task<PlanEntryModel?> GetById(int id)
    {
        var query = new PlanEntryByIdQuery 
        { 
            Id = id
        };

        var model = await _mediator.Send(query);

        return model;
    }

    [HttpPost]
    [Route("/api/plan-entry/add")]
    public async Task<ResultResponse<int?>> Add(PlanEntryModel model)
    {
        var command = new AddPlanEntryCommand 
        { 
            Model = model 
        };

        var response = await _mediator.Send(command);

        return response;
    }

    [HttpPut]
    [Route("/api/plan-entry/update")]
    public async Task<ResultResponse<int?>> Update(PlanEntryModel model)
    {
        var command = new UpdatePlanEntryCommand 
        { 
            Model = model 
        };

        var response = await _mediator.Send(command);

        return response;
    }

    [HttpDelete]
    [Route("/api/plan-entry/delete/{id}")]
    public async Task<Response> DeleteById(int id)
    {
        var command = new DeletePlanEntryCommand
        {
            Id = id
        };

        var response = await _mediator.Send(command);

        return response;
    }
}
