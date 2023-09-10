using MediatR;
using Microsoft.Extensions.Logging;
using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Commands;
using TRS.FinalPlantasy.Application.Abstractions.Validations;

namespace TRS.FinalPlantasy.Application.Planning.Commands;

internal class AddPlanEntryCommandHandler : IRequestHandler<AddPlanEntryCommand, ResultResponse<int?>>
{
    private readonly IAddPlanApplicationService _applicationService;
    private readonly ILogger<AddPlanEntryCommandHandler> _logger;

    public AddPlanEntryCommandHandler(
        IAddPlanApplicationService applicationService,
         ILogger<AddPlanEntryCommandHandler> logger)
    {
        _applicationService = applicationService;
        _logger = logger;
    }

    public async Task<ResultResponse<int?>> Handle(AddPlanEntryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Model == null)
            {
                throw new NullReferenceException(nameof(request.Model));
            }

            return await _applicationService.AddAsync(request.Model, cancellationToken);
        }
        catch (Exception e)
        {
            const string message = "The add operation failed.";

            _logger.LogError(e, message);

            return ResultResponse<int?>.WithError(null, message);
        }
    }
}
