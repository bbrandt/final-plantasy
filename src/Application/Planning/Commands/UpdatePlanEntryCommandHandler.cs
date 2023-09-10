using MediatR;
using Microsoft.Extensions.Logging;
using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Commands;
using TRS.FinalPlantasy.Application.Abstractions.Validations;

namespace TRS.FinalPlantasy.Application.Planning.Commands;

internal class UpdatePlanEntryCommandHandler : IRequestHandler<UpdatePlanEntryCommand, ResultResponse<int?>>
{
    private readonly IUpdatePlanApplicationService _applicationService;
    private readonly ILogger<AddPlanEntryCommandHandler> _logger;

    public UpdatePlanEntryCommandHandler(
        IUpdatePlanApplicationService applicationService,
        ILogger<AddPlanEntryCommandHandler> logger)
    {
        _applicationService = applicationService;
        _logger = logger;
    }

    public async Task<ResultResponse<int?>> Handle(UpdatePlanEntryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Model == null)
            {
                throw new NullReferenceException(nameof(request.Model));
            }

            return await _applicationService.UpdateAsync(request.Model, cancellationToken);
        }
        catch (Exception e)
        {
            const string message = "The update operation failed.";

            _logger.LogError(e, message);

            return ResultResponse<int?>.WithError(null, message);
        }
    }
}
