using TRS.FinalPlantasy.Domain.Model.Planning;

namespace TRS.FinalPlantasy.Application.Abstractions.Planning.Repositories;

public interface IPlanEntryRepository
{
    /// <summary>
    /// Add a plan entry entity to the repository
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddAsync(PlanEntry entity, CancellationToken cancellationToken);
}
