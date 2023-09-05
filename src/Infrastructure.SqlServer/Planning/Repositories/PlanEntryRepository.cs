using Microsoft.EntityFrameworkCore;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Repositories;
using TRS.FinalPlantasy.Domain.Model.Planning;
using TRS.FinalPlantasy.Infrastructure.SqlServer.Common;

namespace TRS.FinalPlantasy.Infrastructure.SqlServer.Planning.Repositories;

internal class PlanEntryRepository : GenericRepository<PlanEntry>, IPlanEntryRepository
{
    public PlanEntryRepository(DbContext context) : base(context)
    {
    }
}
