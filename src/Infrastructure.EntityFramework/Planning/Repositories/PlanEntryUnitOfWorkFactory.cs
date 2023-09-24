using TRS.FinalPlantasy.Application.Abstractions.Planning.Repositories;
using TRS.FinalPlantasy.Infrastructure.EntityFramework.Common;

namespace TRS.FinalPlantasy.Infrastructure.EntityFramework.Planning.Repositories;

internal class PlanEntryUnitOfWorkFactory
    :
    GenericUnitOfWorkFactory<IPlanEntryUnitOfWork, IUnitOfWorkCreator<IPlanEntryUnitOfWork>>, IPlanEntryUnitOfWorkFactory
{
    public PlanEntryUnitOfWorkFactory(IServiceProvider serviceProvider)
        :
        base(serviceProvider)
    {
    }
}
