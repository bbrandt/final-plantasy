using Microsoft.EntityFrameworkCore;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Repositories;
using TRS.FinalPlantasy.Domain.Model.Planning;
using TRS.FinalPlantasy.Infrastructure.EntityFramework.Common;
using TRS.FinalPlantasy.Infrastructure.EntityFramework.Planning;

namespace TRS.FinalPlantasy.Infrastructure.EntityFramework.Planning.Repositories;

internal class PlanEntryRepository : GenericRepository<PlanEntry>, IPlanEntryRepository
{
    public PlanEntryRepository(PlanningContext context) : base(context)
    {
    }

    public async Task<PlanEntry?> FindAsync(int id, CancellationToken cancellationToken)
    {
        var found = await Context.Set<PlanEntry>()
            .SingleAsync(x => x.Id == id, cancellationToken);

        return found;
    }

    public async Task RemoveAsync(int id, CancellationToken cancellationToken)
    {
        var found = await FindAsync(id, cancellationToken);

        /*
         * It is okay if the entity has already been deleted
         */
        if (found == null)
        {
            return;
        }

        Context.Set<PlanEntry>().Remove(found);
    }
}
