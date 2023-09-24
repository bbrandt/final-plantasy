using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Repositories;
using TRS.FinalPlantasy.Infrastructure.EntityFramework.Common;

namespace TRS.FinalPlantasy.Infrastructure.EntityFramework.Planning.Repositories;

internal class PlanEntryUnitOfWork : GenericUnitOfWork, IPlanEntryUnitOfWork
{
    public PlanEntryUnitOfWork(
        IServiceScope serviceScope,
        DbContext context,
        IPlanEntryRepository planEntryRepository)
        :
        base(
            serviceScope,
            context)
    {
        PlanEntries = planEntryRepository;
    }

    public IPlanEntryRepository PlanEntries { get; }
}
