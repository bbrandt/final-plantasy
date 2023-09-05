using MediatR;
using Microsoft.Extensions.Logging;
using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Commands;
using TRS.FinalPlantasy.Application.Abstractions.Validations;

namespace TRS.FinalPlantasy.Application.Planning.Commands;

internal class NewPlanEntryCommandHandler : IRequestHandler<NewPlanEntryCommand, ResultResponse<int?>>
{
    private readonly IPlanEntryApplicationService _applicationService;
    private readonly ILogger<NewPlanEntryCommandHandler> _logger;

    public NewPlanEntryCommandHandler(
        IPlanEntryApplicationService applicationService,
         ILogger<NewPlanEntryCommandHandler> logger)
    {
        _applicationService = applicationService;
        _logger = logger;
    }

    public async Task<ResultResponse<int?>> Handle(NewPlanEntryCommand request, CancellationToken cancellationToken)
    {
        if (request.Model == null)
        {
            const string message = "The request model is undefined.";

            _logger.LogError(message);

            throw new NullReferenceException(message);
        }

        return await _applicationService.AddPlanEntryAsync(request.Model, cancellationToken);
    }
}
