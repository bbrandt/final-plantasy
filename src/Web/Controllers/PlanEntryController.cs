using Microsoft.AspNetCore.Mvc;

namespace TRS.FinalPlantasy.Web.Controllers;

[ApiController]
[Route("/api/plan-entry")]
public class PlanEntryController : ControllerBase
{
    private readonly ILogger<PlanEntryController> _logger;

    public PlanEntryController(ILogger<PlanEntryController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("/api/plan-entry/list")]
    public IEnumerable<PlanEntry> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new PlanEntry
        {
            Id = index,
            EventDate = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Amount = Random.Shared.Next(-20, 55)                
        })
        .ToArray();
    }

    [HttpPost]
    [Route("/api/plan-entry/add-or-update")]
    public async Task<Response> AddOrUpdate(PlanEntry model)
    {
        await Task.Delay(0);

        return new Response { Message = "TESTING" };
    }
}

public class Response
{ 
    public string Message { get; set; }
}

public class ValidationMessage
{

}

public enum ValidationType
{ 
}