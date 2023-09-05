using TRS.FinalPlantasy.Application.Abstractions.Planning.Repositories;
using TRS.FinalPlantasy.Infrastructure.SqlServer.Common;

namespace TRS.FinalPlantasy.Infrastructure.SqlServer.Planning.Repositories;

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
