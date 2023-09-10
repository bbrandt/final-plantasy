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

    /// <summary>
    /// Find an entry by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PlanEntry?> FindAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Removes an entry
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RemoveAsync(int id, CancellationToken cancellationToken);
}
