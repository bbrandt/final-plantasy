using MediatR;
using Microsoft.Extensions.Logging;
using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Queries;

namespace TRS.FinalPlantasy.Application.Planning.Queries;

internal class PlanTimelineQueryHandler : IRequestHandler<PlanTimelineQuery, PlanTimelineModel>
{
    private readonly IPlanTimelineApplicationService _timelineApplicationService;
    private readonly ILogger<PlanTimelineQueryHandler> _logger;

    public PlanTimelineQueryHandler(
        IPlanTimelineApplicationService timelineApplicationService,
        ILogger<PlanTimelineQueryHandler> logger)
    {
        _timelineApplicationService = timelineApplicationService;
        _logger = logger;
    }

    public Task<PlanTimelineModel> Handle(PlanTimelineQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var timeline = _timelineApplicationService.CalculatePlanTimeline(request.EndDate);

            return Task.FromResult(timeline);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to calculate plan timeline.");

            throw;
        }
    }
}
