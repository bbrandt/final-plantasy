using TRS.FinalPlantasy.Application.Abstractions.Validations;

namespace TRS.FinalPlantasy.Application.Abstractions.Planning;

public interface IPlanningApplicationService
{
    /// <summary>
    /// Add a new plan entry. The application service is the direct client of the domain.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ResultResponse<int?>> AddPlanEntryAsync(PlanEntryModel model, CancellationToken cancellationToken);
}
