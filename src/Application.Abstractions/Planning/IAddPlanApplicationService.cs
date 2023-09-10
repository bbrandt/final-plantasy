using TRS.FinalPlantasy.Application.Abstractions.Validations;

namespace TRS.FinalPlantasy.Application.Abstractions.Planning;

public interface IAddPlanApplicationService
{
    /// <summary>
    /// Add a plan entry. The application service is the direct client of the domain.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ResultResponse<int?>> AddAsync(PlanEntryModel model, CancellationToken cancellationToken);
}
