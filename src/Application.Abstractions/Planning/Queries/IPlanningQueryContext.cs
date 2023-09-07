using TRS.FinalPlantasy.Domain.Model.Planning;

namespace TRS.FinalPlantasy.Application.Abstractions.Planning.Queries;

public interface IPlanningQueryContext
{
    public IQueryable<PlanEntry> PlanEntries { get; }
}
