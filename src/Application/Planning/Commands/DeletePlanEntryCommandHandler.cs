using MediatR;
using Microsoft.Extensions.Logging;
using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Commands;
using TRS.FinalPlantasy.Application.Abstractions.Validations;

namespace TRS.FinalPlantasy.Application.Planning.Commands;

internal class DeletePlanEntryCommandHandler : IRequestHandler<DeletePlanEntryCommand, Response>
{
    private readonly IDeletePlanApplicationService _applicationService;
    private readonly ILogger<AddPlanEntryCommandHandler> _logger;

    public DeletePlanEntryCommandHandler(
        IDeletePlanApplicationService applicationService,
        ILogger<AddPlanEntryCommandHandler> logger)
    {
        _applicationService = applicationService;
        _logger = logger;
    }

    public async Task<Response> Handle(DeletePlanEntryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await _applicationService.DeleteAsync(request.Id, cancellationToken);
        }
        catch (Exception e)
        {
            const string message = "Delete operation failed.";

            _logger.LogError(e, message);

            return Response.WithError(message);
        }
    }
}
