using TRS.FinalPlantasy.Domain.Model.Planning;

namespace TRS.FinalPlantasy.Application.Abstractions.Planning;

public interface IPlanEntryRepository
{
    void Add(PlanEntry entity);
}
