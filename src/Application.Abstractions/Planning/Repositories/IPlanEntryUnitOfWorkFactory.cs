namespace TRS.FinalPlantasy.Application.Abstractions.Planning.Repositories;

public interface IPlanEntryUnitOfWorkFactory
{
    Task<IPlanEntryUnitOfWork> CreateAsync(CancellationToken cancellationToken = default);
}