using Microsoft.EntityFrameworkCore;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Queries;
using TRS.FinalPlantasy.Domain.Model.Planning;

namespace TRS.FinalPlantasy.Infrastructure.SqlServer.Planning.Queries
{
    internal class PlanningQueryContext : IPlanningQueryContext
    {
        private readonly PlanningContext _context;

        public PlanningQueryContext(PlanningContext context)
        {
            _context = context;
        }

        public IQueryable<PlanEntry> PlanEntries => _context.Set<PlanEntry>().AsNoTracking();
    }
}
