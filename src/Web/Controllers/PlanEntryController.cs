using Microsoft.AspNetCore.Mvc;

namespace TRS.FinalPlantasy.Web.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PlanEntryController : ControllerBase
    {
        private readonly ILogger<PlanEntryController> _logger;

        public PlanEntryController(ILogger<PlanEntryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
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
    }
}