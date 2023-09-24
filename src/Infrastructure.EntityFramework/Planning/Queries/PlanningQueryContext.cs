using Microsoft.EntityFrameworkCore;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Queries;
using TRS.FinalPlantasy.Domain.Model.Planning;

namespace TRS.FinalPlantasy.Infrastructure.EntityFramework.Planning.Queries
{
    internal class PlanningQueryContext : IPlanningQueryContext
    {
        private readonly PlanningContext _context;

        public PlanningQueryContext(PlanningContext context)
        {
            _context = context;
        }

        public IQueryable<PlanEntry> PlanEntries => _context.Set<PlanEntry>().AsNoTracking();

        public IQueryable<PlanType> PlanTypes => _context.Set<PlanType>().AsNoTracking();

        public IQueryable<PlanRepeatOn> PlanRepeatOns => _context.Set<PlanRepeatOn>().AsNoTracking();
    }
}
