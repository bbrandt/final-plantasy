﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Commands;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Queries;
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
    public async Task<PlanEntryModel> GetById(int id)
    {
        var query = new PlanEntryByIdQuery 
        { 
            Id = id
        };

        var models = await _mediator.Send(query);

        return models;
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
