using TRS.FinalPlantasy.Application.Abstractions.Repositories;

namespace TRS.FinalPlantasy.Application.Abstractions.Planning.Repositories;

public interface IPlanEntryUnitOfWork : IUnitOfWork
{
    IPlanEntryRepository PlanEntries { get; }
}
