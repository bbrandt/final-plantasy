using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Repositories;
using TRS.FinalPlantasy.Application.Abstractions.Validations;

namespace TRS.FinalPlantasy.Application.Planning;

internal class DeletePlanApplicationService : IDeletePlanApplicationService
{
    private readonly IPlanEntryUnitOfWorkFactory _unitOfWorkFactory;

    public DeletePlanApplicationService(
        IPlanEntryUnitOfWorkFactory unitOfWorkFactory)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
    }

    public async Task<Response> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        using var unitOfWork = await _unitOfWorkFactory.CreateAsync(cancellationToken);

        await unitOfWork.PlanEntries.RemoveAsync(id, cancellationToken);

        await unitOfWork.CompleteAsync(cancellationToken);

        return Response.Empty();
    }
}
